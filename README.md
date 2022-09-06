# Fable.Requests [![Nuget](https://img.shields.io/nuget/v/Fable.Requests.svg?colorB=green)](https://www.nuget.org/packages/Fable.Requests)

Fable library for making HTTP requests targeting Python and using the [requests](https://pypi.org/project/requests/) library under the hood.

> This is the initial release still of the library and is considers a placeholder for the implementation of the API. The current state of the library is _not_ usable and only had the `Requests.get` function! 

```fs
open Fable.Requests

let response = Requests.get "https://api.thecatapi.com/v1/images/search"

printfn $"Status code: {response.status_code}"
printfn $"Response: {response.text}"
```

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