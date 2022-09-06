namespace Fable.Requests

open Fable.Core

type private InteropResponseType =
    abstract status_code : int
    abstract text : string
    abstract encoding : string
    abstract headers : obj

[<RequireQualifiedAccess>]
module Interop = 
    [<Emit("$1[$0]")>]
    let get<'T> (key: string) (dict: obj) : 'T = nativeOnly
    [<Emit("list($0.keys())")>]
    let keys (dict: obj) : string array = nativeOnly

type Response = {
    statusCode: int
    text: string
    headers: Map<string, string>
    encoding: string
}

[<Erase>]
type private RequestsApi = 
    [<Emit "$0.get($1)">]
    abstract get: url:string -> InteropResponseType
    [<Emit "$0.get($1, headers=$2)">]
    abstract getWithHeaders: url:string * headers:obj -> InteropResponseType
    [<Emit "$0.post($1, data=$2)">]
    abstract post: url: string * data: string -> InteropResponseType
    [<Emit "$0.post($1, data=$2, headers=$3)">]
    abstract postWithHeaders: url: string * data: string * headers:obj -> InteropResponseType
    [<Emit "$0.put($1, data=$2)">]
    abstract put: url: string * data: string -> InteropResponseType
    [<Emit "$0.put($1, data=$2, headers=$3)">]
    abstract putWithHeaders: url: string * data: string * headers:obj -> InteropResponseType
    [<Emit "$0.delete($1, data=$2)">]
    abstract delete: url: string * data: string -> InteropResponseType
    [<Emit "$0.delete($1, data=$2, headers=$3)">]
    abstract deleteWithHeaders: url: string * data: string * headers:obj -> InteropResponseType

type Requests() = 
    [<ImportAll("requests")>]
    static member private requestsApi: RequestsApi = nativeOnly
    /// <summary>
    /// Sends a GET request to the specified URL and returns a response.
    /// </summary>
    static member get(url: string, ?headers:Map<string, string>) : Response =
        let response: InteropResponseType =
            match headers with
            | None -> Requests.requestsApi.get(url)
            | Some values -> 
                let headersDict = PyInterop.createObj [ for pair in values -> pair.Key, box pair.Value ]
                Requests.requestsApi.getWithHeaders(url, headersDict)

        {
            statusCode = response.status_code
            text = response.text
            encoding = response.text
            headers = Map.ofList [
                for headerName in Interop.keys response.headers do
                    let headerValue =  Interop.get<string> headerName response.headers
                    headerName, headerValue
            ]
        }

    /// <summary>
    /// Sends a POST request to the specified URL and returns a response.
    /// </summary>
    static member post(url: string, data: string, ?headers:Map<string, string>) : Response =
        let response: InteropResponseType =
            match headers with
            | None -> Requests.requestsApi.post(url, data)
            | Some values -> 
                let headersDict = PyInterop.createObj [ for pair in values -> pair.Key, box pair.Value ]
                Requests.requestsApi.postWithHeaders(url, data, headersDict)

        {
            statusCode = response.status_code
            text = response.text
            encoding = response.text
            headers = Map.ofList [
                for headerName in Interop.keys response.headers do
                    let headerValue =  Interop.get<string> headerName response.headers
                    headerName, headerValue
            ]
        }

    /// <summary>
    /// Sends a PUT request to the specified URL and returns a response.
    /// </summary>
    static member put(url: string, data: string, ?headers:Map<string, string>) : Response =
        let response: InteropResponseType =
            match headers with
            | None -> Requests.requestsApi.put(url, data)
            | Some values -> 
                let headersDict = PyInterop.createObj [ for pair in values -> pair.Key, box pair.Value ]
                Requests.requestsApi.putWithHeaders(url, data, headersDict)

        {
            statusCode = response.status_code
            text = response.text
            encoding = response.text
            headers = Map.ofList [
                for headerName in Interop.keys response.headers do
                    let headerValue =  Interop.get<string> headerName response.headers
                    headerName, headerValue
            ]
        }

    /// <summary>
    /// Sends a DELETE request to the specified URL and returns a response.
    /// </summary>
    static member delete(url: string, data: string, ?headers:Map<string, string>) : Response =
        let response: InteropResponseType =
            match headers with
            | None -> Requests.requestsApi.delete(url, data)
            | Some values -> 
                let headersDict = PyInterop.createObj [ for pair in values -> pair.Key, box pair.Value ]
                Requests.requestsApi.deleteWithHeaders(url, data, headersDict)

        {
            statusCode = response.status_code
            text = response.text
            encoding = response.text
            headers = Map.ofList [
                for headerName in Interop.keys response.headers do
                    let headerValue =  Interop.get<string> headerName response.headers
                    headerName, headerValue
            ]
        }