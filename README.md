# Owin.SelfHost.Runner

Run your OWIN application on all platforms the easy way !   

[![Build status](https://ci.appveyor.com/api/projects/status/gbr5w9lwrm66ld0q?svg=true)](https://ci.appveyor.com/project/JulienPavon/owin-selfhost-runner) [![NuGet version](https://badge.fury.io/nu/Owin.SelfHost.Runner.svg)](https://badge.fury.io/nu/Owin.SelfHost.Runner)

## How to use it

Create a console application and add this line of code in your Main method :

    private static void Main(string[] args)
    {
      ProgramRunner<Startup>.Run(args);
    }

Now your application can be configured with a domain and a port as follow

> YourApplication.exe --domain=https://mydomain.com --port=8080

Also you can show an help page :

> YourApplication.exe --help

## Licence

The MIT License (MIT)

Copyright (c) 2016 JulienPavon

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
