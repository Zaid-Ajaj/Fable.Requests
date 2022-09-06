# Fable.Requests [![Nuget](https://img.shields.io/nuget/v/Fable.Requests.svg?colorB=green)](https://www.nuget.org/packages/Fable.Requests)

Fable library for making HTTP requests targeting Python and using the [requests](https://pypi.org/project/requests/) library under the hood.

> This is the initial release still of the library and is considers a placeholder for the full implementation of the API. The current state of the library is barely usable and contributions are more than welcome!

```fs
open Fable.Requests

let response = Requests.get "https://api.thecatapi.com/v1/images/search"

printfn $"Status code: {response.statusCode}"
printfn $"Response: {response.text}"
```

There are these functions:
 - `Request.get(url: string)`
 - `Request.get(url: string, ?headers:Map<string, string>)` 
 - `Request.post(url: string, data: string)`
 - `Request.post(url: string, data: string, ?headers:Map<string, string>)` 
 - `Request.put(url: string, data: string)`
 - `Request.put(url: string, data: string, ?headers:Map<string, string>)` 
 - `Request.delete(url: string, data: string)`
 - `Request.delete(url: string, data: string, ?headers:Map<string, string>)` 

## Installation

You can install this library using [Femto](https://github.com/Zaid-Ajaj/Femto) when you are using [Poetry](https://python-poetry.org/) to manage your Python dependencies

```
femto install Fable.Requests
```

Alternatively, you can install it manually

```
dotnet add package Fable.Requests
poetry add requests@2.28.1
```

## Compile and run your project
Requires Fable v4 (snake island) 

```bash
cd ./path/to/proj
dotnet fable --lang python
poetry run python program.py
```