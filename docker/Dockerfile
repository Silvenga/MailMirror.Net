FROM ubuntu:trusty

RUN \
	DEBIAN_FRONTEND=noninteractive apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF && \
	echo "deb http://download.mono-project.com/repo/debian wheezy main" | sudo tee /etc/apt/sources.list.d/mono-xamarin.list && \
	DEBIAN_FRONTEND=noninteractive apt-get update && \
	DEBIAN_FRONTEND=noninteractive apt-get install mono-complete -y

RUN \
	echo "postfix postfix/main_mailer_type string Internet site" > preseed.txt && \
	echo "postfix postfix/mailname string mail.example.com" >> preseed.txt && \
	debconf-set-selections preseed.txt && \
	DEBIAN_FRONTEND=noninteractive apt-get install -q -y postfix
	
ADD bin/* /root/
	
CMD /bin/bash /root/start.sh