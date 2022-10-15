// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using ExceptionConditioning.Console;
using System.Diagnostics;
using System.Net;

CosmosDbClient client = new();
const string docId = "1";

await TryToDeleteDocumentV1();
await TryToDeleteDocumentV2();
TryToDeleteDocumentV3V1();
TryToDeleteDocumentV3V2();



async Task<HttpStatusCode> TryToDeleteDocumentV1()
{
    // 1000 ms
    var document = await client.GetDocument(docId);

    if (document != null)
    {
        client.DeleteDocument(docId);
        return HttpStatusCode.OK;
    }

    return HttpStatusCode.NotFound;
}

async Task<HttpStatusCode> TryToDeleteDocumentV2()
{
    // 500 ms
    var documentExists = await client.DocumentExistsAsync(docId);

    if (!documentExists)
    {
        client.DeleteDocument(docId);
        return HttpStatusCode.OK;
    }

    return HttpStatusCode.NotFound;
}


HttpStatusCode TryToDeleteDocumentV3V1()
{
    try
    {
        client.DeleteDocument(docId);
        return HttpStatusCode.OK;
    }
    catch (DocumentClientException ex)
    {
        if (ex.HttpStatusCode == HttpStatusCode.NotFound)
            return HttpStatusCode.NotFound;

        return HttpStatusCode.NoContent;
    }
}


HttpStatusCode TryToDeleteDocumentV3V2()
{
    try
    {
        client.DeleteDocument(docId);
        return HttpStatusCode.OK;
    }
    catch (DocumentClientException ex) when (ex.HttpStatusCode == HttpStatusCode.NotFound)
    {
        return HttpStatusCode.NotFound;
    }
}



Console.ReadLine();
