FROM mono

RUN apt-get update && apt-get install -y nunit-console vim

COPY . /root/ECSLight

