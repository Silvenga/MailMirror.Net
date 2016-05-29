![](/docs/icon.png)

# MailMirror.Net

[![Build](https://img.shields.io/appveyor/ci/Silvenga/mailmirror-net.svg?maxAge=2592000&style=flat-square&maxAge=300)](https://ci.appveyor.com/project/Silvenga/mailmirror-net) 
[![Docker](https://img.shields.io/badge/docker-silvenga%2Fmailmirror.net-blue.svg?maxAge=2592000&style=flat-square)](https://hub.docker.com/r/silvenga/mailmirror.net/)

A development mail server that mirrors any emails sent to it, accessible with a REST'ful API. Ideal for integration testing and troubleshooting. 

## Usage

```bash
docker pull silvenga/mailmirror.net

# 25/smtp, 3000/api
docker run -p 9000:9000 -p 25:25 -d silvenga/mailmirror.net

# Get all messages sent to container
curl -X GET "http://localhost:9000/api/messages"
```

## TODO

- [ ] Expire messages automatically
- [ ] Create UI (download EML, delete messages, search, etc.) Aurelia?