namespace Fable.Requests

open Fable.Core

type Response = 
    abstract status_code : int with get
    abstract text : string with get

[<Erase>]
type private RequestsApi = 
    abstract get : string -> Response

type Requests() = 
    [<ImportAll("requests")>]
    static member private requestsApi: RequestsApi = nativeOnly
    static member get(url: string) : Response =
        Requests.requestsApi.get(url)