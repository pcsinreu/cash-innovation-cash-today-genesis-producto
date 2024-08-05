
FROM ubuntu:18.04


LABEL maintainer="jose.bustamante@prosegur.com"


RUN apt-get update

RUN apt-get install -y gnupg2

RUN apt-get install wget -y

RUN wget -qO - https://releases.jfrog.io/artifactory/api/gpg/key/public | apt-key add -;

RUN echo "deb https://releases.jfrog.io/artifactory/jfrog-debs xenial contrib" | tee -a /etc/apt/sources.list;

RUN apt update;

RUN apt install -y jfrog-cli;


RUN /usr/bin/jfrog rt c rt-server-1 --url=https://procde.prosegur.com/artifactory --user=$(artifactory_user) --password=$(artifactory_pass) --interactive=false

RUN /usr/bin/jfrog rt ping


WORKDIR /jfrog


COPY $(Build.BuildId).zip /jfrog

RUN ls -ltrh /jfrog


CMD jfrog rt u "$(Build.BuildId).zip" $(artifactory_repo) --build-number=$(Build.BuildNumber) --build-name="$(Build.DefinitionName)" ; jfrog rt bp "$(Build.DefinitionName)" $(Build.BuildNumber)
