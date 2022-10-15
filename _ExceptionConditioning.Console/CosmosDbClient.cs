using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionConditioning.Console;
public class CosmosDbClient
{
    /// <summary>
    /// Deletes the document if found, otherwise throws a DocumentClientException with NotFound code
    /// </summary>
    /// <param name="id">The document id</param>
    /// <exception cref="DocumentClientException">The exception object</exception>
    public void DeleteDocument(string id)
    {
        throw new DocumentClientException(HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Gets the document if found, otherwise null
    /// </summary>
    /// <param name="id">The document id</param>
    /// <returns>returns the object as string</returns>
    public async Task<object> GetDocument(string id)
    {
        await Task.Delay(1000);
        return "Document";
    }

    /// <summary>
    /// Checks if the document exists
    /// </summary>
    /// <param name="id">The document id</param>
    /// <returns>returns true if found, otherwise false</returns>
    public async Task<bool> DocumentExistsAsync(string id)
    {
        await Task.Delay(500);
        return true;
    }
}


public class DocumentClientException : Exception
{
    public HttpStatusCode HttpStatusCode { get; set; }

    public DocumentClientException(HttpStatusCode httpStatusCode)
    {
        HttpStatusCode = httpStatusCode;
    }
}
