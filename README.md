# Liquidacoes
A sample code showing how to build MICROservices using [Nancy](http://nancyfx.org/) and [VS Code](https://code.visualstudio.com)

This project is designed to give you inspiration so you can build your own Microservices. It works, but I did not implement everything. Pull requests are welcome!

## About this project
In a Microservice perspective
* It's a simple HTTP API which run anywhere;
* It's a Stateless Microservice but you can turn it into Stateful if you want;
* It has a simple InMemory Event Store implemented;
* It uses the Log4net for logging;
* It can receive an user ID from an API Gateway to process requests;
* It was written with functional programming style in mind but it is not 100% in functional style;

In a Nancy perspective, you can see examples of
* Content Negotiation;
* Bootstrapper;
* AppSettings;
* Nancy Model Binding;
* Fluent Validation;
* TinyIoC Container;
* And so on...

With C# 6 we have some exemples of
* Tuples
* Inline try.Parse
* Expression bodies
* Auto-property initializers
* Etc...

## The presentation

Slides of the presentation are available on the [SlideShare](https://www.slideshare.net/celsojunior351/microservices-with-nancy-and-vs-code)
