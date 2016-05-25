#!/bin/bash

chown piper:piper /opt -R

service rsyslog start
service postfix start

mono /opt/MailMirror.Net.Api.exe