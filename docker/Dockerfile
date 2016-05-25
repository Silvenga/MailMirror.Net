FROM ubuntu:trusty

RUN \
	DEBIAN_FRONTEND=noninteractive apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF && \
	#echo "deb http://download.mono-project.com/repo/debian wheezy main" | sudo tee /etc/apt/sources.list.d/mono-xamarin.list && \
	echo "deb http://download.mono-project.com/repo/debian nightly main" | sudo tee /etc/apt/sources.list.d/mono-nightly.list && \
	DEBIAN_FRONTEND=noninteractive apt-get update && \
	DEBIAN_FRONTEND=noninteractive apt-get install mono-complete -y

RUN \
	echo "postfix postfix/main_mailer_type string Internet site" > preseed.txt && \
	echo "postfix postfix/mailname string mailmirror.silvenga.com" >> preseed.txt && \
	debconf-set-selections preseed.txt && \
	DEBIAN_FRONTEND=noninteractive apt-get install -q -y postfix nano unzip
	
RUN \
	echo "piper    unix  -       n       n       -       -       pipe" >> /etc/postfix/master.cf && \
	echo "  flags=X user=piper argv=/usr/bin/mono /opt/MailMirror.Net.Piper.exe id test test" >> /etc/postfix/master.cf && \
	useradd piper && \
	postconf -e mynetworks="0.0.0.0/0" && \
	postconf -e default_transport="piper"
   
ADD start.sh /opt/
	
ADD https://ci.appveyor.com/api/projects/Silvenga/mailmirror-net/artifacts/src/MailMirror.Net.Api/bin/api.zip /tmp/api.zip
ADD https://ci.appveyor.com/api/projects/Silvenga/mailmirror-net/artifacts/src/MailMirror.Net.Piper/bin/piper.zip /tmp/piper.zip
	
RUN \
	unzip -o /tmp/api.zip -d /opt/ && \
	unzip -o /tmp/piper.zip -d /opt/ && \
	rm /tmp/*
	
EXPOSE 25
EXPOSE 9000
	
CMD /bin/bash /opt/start.sh