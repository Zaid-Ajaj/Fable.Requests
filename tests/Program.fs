open Fable.Requests

let response = Requests.get "https://api.thecatapi.com/v1/images/search"

printfn $"Status code: {response.status_code}"
printfn $"Response: {response.text}"
