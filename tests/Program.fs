open Fable.Requests

let printResponse(response: Response) =
    printfn $"Status code: {response.statusCode}"
    printfn $"Response: {response.text}"
    printfn $"Encoding: {response.encoding}"
    printfn $"Headers:"
    for pair in response.headers do
        printfn $"| -- {pair.Key} -> {pair.Value}"

let response = Requests.get "https://api.thecatapi.com/v1/images/search"

printResponse response

let headers = Map.ofList [ "X-VALUE", "1" ]
let responseWithHeaders = Requests.get("https://api.thecatapi.com/v1/images/search", headers=headers)

printResponse responseWithHeaders
