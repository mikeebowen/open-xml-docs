---

api_name:
- Microsoft.Office.DocumentFormat.OpenXML.Packaging
api_type:
- schema
ms.assetid: dd42a9a3-5c16-4cab-ad6d-506cf822ec7a
title: Introduction to markup compatibility
ms.suite: office

ms.author: o365devx
author: o365devx
ms.topic: conceptual
ms.date: 01/08/2025
ms.localizationpriority: high
---

# Introduction to markup compatibility

This topic introduces the markup compatibility features included in the Open XML SDK for Office.

## Introduction

Suppose you have a Microsoft Word 365 document that employs a feature introduced in Microsoft Office 365. When you open that document in Microsoft Word 2016, an earlier version, what should happen? Ideally, you want the document to remain interoperable with Word 2016, even though Word 2016 will not understand the new feature.

Consider also what should happen if you open that document in a hypothetical later version of Office. Here too, you want the document to work as expected. That is, you want the later version of Office to understand and support a feature employed in a document produced by Word 365.

Open XML anticipates these scenarios. The Office Open XML File Formats specification describes facilities for achieving the above desired outcomes in [ECMA-376, Second Edition, Part 3 - Markup Compatibility and Extensibility](https://www.ecma-international.org/publications-and-standards/standards/ecma-376/).

The Open XML SDK supports markup compatibility in a way that makes it easy for you to achieve the above desired outcomes for and Office 365 without having to necessarily become an expert in the specification details.

## What is markup compatibility?

Open XML defines formats for word-processing, spreadsheet and presentation documents in the form of specific markup languages, namely WordprocessingML, SpreadsheetML, and PresentationML. With respect to the Open XML file formats, markup compatibility is the ability for a document expressed in one of the above markup languages to facilitate interoperability between applications, or versions of an application, with different feature sets. This is supported through the use of a defined set of XML elements and attributes in the Markup Compatibility namespace of the Open XML specification. Notice that while the markup is supported in the document format, markup producers and consumers, such as Microsoft Word, must support it as well. In other words, interoperability is a function of support both in the file format and by applications.

## Markup compatibility in the Open XML file formats specification

Markup compatibility is discussed in [ECMA-376, Second Edition, Part 3 - Markup Compatibility and Extensibility](https://www.ecma-international.org/wp-content/uploads/ECMA-376-3_5th_edition_december_2015.zip), which is recommended reading to understand markup compatibility. The specification defines XML attributes to express compatibility rules, and XML elements to specify alternate content. For example, the `Ignorable` attribute specifies namespaces that can be ignored when they are not understood by the consuming application. Alternate-Content elements specify markup alternatives that can be chosen by an application at run time. For example, Word 2013 can choose only the markup alternative that it recognizes. The complete list of compatibility-rule attributes and alternate-content elements and their details can be found in the specification.

## Open XML SDK support for markup compatibility

The work that the Open XML SDK does for markup compatibility is detailed and subtle. However, the goal can be summarized as: using settings that you assign when you open a document, preprocess the document to:

1. Filter or remove any elements from namespaces that will not be understood (for example, Office 365 document opened in Office 2016 context)
2. Process any markup compatibility elements and attributes as specified in the Open XML specification.

The preprocessing performed is in accordance with ECMA-376, Second Edition: Part 3.13.

The Open XML SDK support for markup compatibility comes primarily in the form of two classes and in the manner in which content is preprocessed in accordance with ECMA-376, Second Edition. The two classes are `OpenSettings` and `MarkupCompatibilityProcessSettings`. Use the former to provide settings that apply to SDK behavior overall. Use the latter to supply one part of those settings, specifically those that apply to markup compatibility.

## Set the stage when you open

When you open a document using the Open XML SDK, you have the option of using an overload with a signature that accepts an instance of the <xref:DocumentFormat.OpenXml.Packaging.OpenSettings> class as a parameter. You use the open settings class to provide certain important settings that govern the behavior of the SDK. One set of settings in particular, stored in the <xref:DocumentFormat.OpenXml.Packaging.OpenSettings.MarkupCompatibilityProcessSettings*> property, determines how markup compatibility elements and attributes are processed. You set the property to an instance of the <xref:DocumentFormat.OpenXml.Packaging.MarkupCompatibilityProcessSettings> class prior to opening a document.

The class has the following properties:

- <xref:DocumentFormat.OpenXml.Packaging.MarkupCompatibilityProcessSettings.ProcessMode*> - Determines the parts that are preprocessed.

- <xref:DocumentFormat.OpenXml.Packaging.MarkupCompatibilityProcessSettings.TargetFileFormatVersions*> - Specifies the context that applies to preprocessing.

By default, documents are not preprocessed. If however you do specify open settings and provide markup compatibility process settings, then the document is preprocessed in accordance with those settings.

The following code example demonstrates how to call the Open method with an instance of the open settings class as a parameter. Notice that the `ProcessMode` and `TargetFileFormatVersions` properties are initialized as part of the `MarkupCompatiblityProcessSettings` constructor.

```csharp
    // Create instance of OpenSettings
    OpenSettings openSettings = new OpenSettings();

    // Add the MarkupCompatibilityProcessSettings
    openSettings.MarkupCompatibilityProcessSettings = new MarkupCompatibilityProcessSettings(
            MarkupCompatibilityProcessMode.ProcessAllParts,
            FileFormatVersions.Office2007);

    // Open the document with OpenSettings
    using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filename, true, openSettings))
    {
        // ... more code here
    }
```

## What happens during preprocessing

During preprocessing, the Open XML SDK removes elements and attributes in the markup compatibility namespace, removing the contents of unselected alternate-content elements, and interpreting compatibility-rule attributes as appropriate. This work is guided by the process mode and target file format versions properties.

The `ProcessMode` property determines the parts to be preprocessed. The content in *those* parts is filtered to contain only elements that are understood by the application version indicated in the `TargetFileFormatVersions` property.

> [!WARNING]
> Preprocessing affects what gets saved. When you save a file, the only markup that is saved is that which remains after preprocessing.

## Understand process mode

The process mode specifies which document parts should be preprocessed. You set this property to a member of the <xref:DocumentFormat.OpenXml.Packaging.MarkupCompatibilityProcessMode> enumeration. The default value, `NoProcess`, indicates that no preprocessing is performed. Your application must be able to understand and handle any elements and attributes present in the document markup, including any of the elements and attributes in the Markup Compatibility namespace.

You might want to work on specific document parts while leaving the rest untouched. For example, you might want to ensure minimal modification to the file. In that case, specify `ProcessLoadedPartsOnly` for the process mode. With this setting, preprocessing and the associated filtering is only applied to the loaded document parts, not the entire document.

Finally, there is `ProcessAllParts`, which specifies what the name implies. When you choose this value, the entire document is preprocessed.

## Set the target file format version

The target file format versions property lets you choose to process markup compatibility content in the context of a specific Office version, e.g. Office 365. Set the `TargetFileFormatVersions` property to a member of the <xref:DocumentFormat.OpenXml.FileFormatVersions> enumeration.

The default value, `Office2007`, means the SDK will assume that namespaces defined in Office 2007 are understood, but not namespaces defined in Office 2010 or later. Thus, during preprocessing, the SDK will ignore the namespaces defined in newer Office versions and choose the Office 2007 compatible alternate-content.

When you set the target file format versions property to `Office2013`, the Open XML SDK assumes that all of the namespaces defined in Office 2010 and Office 2013 are understood, does not ignore any content defined under Office 2013, and will choose the Office 2013 compatible alternate-content.
