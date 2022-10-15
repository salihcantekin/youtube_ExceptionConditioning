// See https://aka.ms/new-console-template for more information
using ExceptionConditioning.Console;
using System.Net;
using System.Reflection.Metadata;

Console.WriteLine("Hello, World!");


var client = new CosmosDbClient();
var docId = "1";
// FileLogger.Log("Test");



// Web Api

async Task<HttpStatusCode> TryToDeleteDocumentV1() // 1250 ms
{
    // 1000 ms
    var document = await client.GetDocument(docId);

    if (document != null)
    {
        client.DeleteDocument(docId); // 250 ms
        return HttpStatusCode.OK;
    }

    return HttpStatusCode.NotFound;
}

async Task<HttpStatusCode> TryToDeleteDocumentV2() // 750 ms
{
    // 500 ms
    var documentExists = await client.DocumentExistsAsync(docId);

    if (documentExists)
    {
        client.DeleteDocument(docId); // 250 ms
        return HttpStatusCode.OK;
    }

    return HttpStatusCode.NotFound;
}

HttpStatusCode TryToDeleteDocumentV3V1()
{
    try
    {
        client.DeleteDocument(docId); // 250 ms
        return HttpStatusCode.OK;
    }
    catch (DocumentClientException ex)
    {
        if (ex.HttpStatusCode == HttpStatusCode.NotFound)
            return ex.HttpStatusCode;

        return HttpStatusCode.BadRequest;
    }
}

HttpStatusCode TryToDeleteDocumentV3V2()
{
    try
    {
        client.DeleteDocument(docId); // 250 ms
        return HttpStatusCode.OK;
    }
    catch (DocumentClientException ex) when (ex.HttpStatusCode == HttpStatusCode.NotFound)
    {
        return HttpStatusCode.NotFound;
    }
    catch(Exception)
    {
        return HttpStatusCode.BadRequest;
    }
}

