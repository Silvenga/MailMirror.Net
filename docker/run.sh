#!/bin/bash

docker build -t mailmirror . && docker run -p 25:25 -p 9000:9000 -it mailmirror 