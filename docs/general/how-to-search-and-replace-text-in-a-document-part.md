---

api_name:
- Microsoft.Office.DocumentFormat.OpenXML.Packaging
api_type:
- schema
ms.assetid: cbb4547e-45fa-48ee-872e-8727beec6dfa
title: 'How to: Search and replace text in a document part'
ms.suite: office

ms.author: o365devx
author: o365devx
ms.topic: conceptual
ms.date: 11/01/2017
ms.localizationpriority: high
---
# Search and replace text in a document part

This topic shows how to use the classes in the Open XML SDK for
Office to programmatically search and replace a text value in a word
processing document.



--------------------------------------------------------------------------------
[!include[Structure](../includes/word/packages-and-document-parts.md)]


---------------------------------------------------------------------------------
## Getting a WordprocessingDocument Object

In the sample code, you start by opening the word processing file by
instantiating the <xref:DocumentFormat.OpenXml.Packaging.WordprocessingDocument> class as shown in
the following `using` statement. In the same
statement, you open the word processing file `document` by using the
<xref:DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open%2A> method, with the Boolean parameter set
to `true` to enable editing the document.

### [C#](#tab/cs-1)
[!code-csharp[](../../samples/word/search_and_replace_text_a_part/cs/Program.cs#snippet1)]

### [Visual Basic](#tab/vb-1)
[!code-vb[](../../samples/word/search_and_replace_text_a_part/vb/Program.vb#snippet1)]
***


[!include[Using Statement](../includes/word/using-statement.md)]


--------------------------------------------------------------------------------
## Sample Code

The following example demonstrates a quick and easy way to search and
replace. It may not be reliable because it retrieves the XML document in
string format. Depending on the regular expression you might
unintentionally replace XML tags and corrupt the document. If you simply
want to search a document, but not replace the contents you can use
`MainDocumentPart.Document.InnerText`.

This example also shows how to use a regular expression to search and
replace the text value, "Hello World!" stored in a word processing file
with the value "Hi Everyone!". To call the method
`SearchAndReplace`, you can use the following
example.

### [C#](#tab/cs-2)
[!code-csharp[](../../samples/word/search_and_replace_text_a_part/cs/Program.cs#snippet2)]

### [Visual Basic](#tab/vb-2)
[!code-vb[](../../samples/word/search_and_replace_text_a_part/vb/Program.vb#snippet2)]
***


After running the program, you can inspect the file to see the change in
the text, "Hello world!"

The following is the complete sample code in both C\# and Visual Basic.

### [C#](#tab/cs)
[!code-csharp[](../../samples/word/search_and_replace_text_a_part/cs/Program.cs#snippet0)]

### [Visual Basic](#tab/vb)
[!code-vb[](../../samples/word/search_and_replace_text_a_part/vb/Program.vb#snippet0)]
***

--------------------------------------------------------------------------------
## See also


- [Open XML SDK class library reference](/office/open-xml/open-xml-sdk)

- [Regular Expressions](/dotnet/standard/base-types/regular-expressions)
