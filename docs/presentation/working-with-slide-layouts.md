---

api_name:
- Microsoft.Office.DocumentFormat.OpenXML.Packaging
api_type:
- schema
ms.assetid: 1ec087c3-8b9e-46a9-9c3c-14586908eb0e
title: Working with slide layouts
ms.suite: office

ms.author: o365devx
author: o365devx
ms.topic: conceptual
ms.date: 09/18/2024
ms.localizationpriority: medium
---

# Working with slide layouts

This topic discusses the Open XML SDK for Office <xref:DocumentFormat.OpenXml.Presentation.SlideLayout> class and how it relates to the Open XML File Format PresentationML schema.

## Slide Layouts in PresentationML

The [!include[ISO/IEC 29500 URL](../includes/iso-iec-29500-link.md)] specification describes the Open XML PresentationML `<sldLayout/>` element used to represent slide layouts in a PresentationML document as follows:

This element specifies an instance of a slide layout. The slide layout
contains in essence a template slide design that can be applied to any
existing slide. When applied to an existing slide all corresponding
content should be mapped to the new slide layout.

The `<sldLayout/>` element is the root element of the PresentationML
Slide Layout part. For more information about the overall structure of
the parts and elements that make up a PresentationML document, see
[Structure of a PresentationML Document](structure-of-a-presentationml-document.md).

The following table lists the child elements of the `<sldLayout/>`
element used when working with slide layouts and the Open XML SDK
classes that correspond to them.

| **PresentationML Element** |    **Open XML SDK Class**           |
|----------------------------|-------------------------------------|
|       `<clrMapOvr/>`        |              <xref:DocumentFormat.OpenXml.Presentation.ColorMapOverride>              |
|          `<cSld/>`          |               <xref:DocumentFormat.OpenXml.Presentation.CommonSlideData>               |
|         `<extLst/>`         | <xref:DocumentFormat.OpenXml.Presentation.ExtensionListWithModification> |
|           `<hf/>`           |                  <xref:DocumentFormat.OpenXml.Presentation.HeaderFooter>                  |
|         `<timing/>`         |                        <xref:DocumentFormat.OpenXml.Presentation.Timing>                        |
|       `<transition/>`       |                    <xref:DocumentFormat.OpenXml.Presentation.Transition>                    |

The following table from the [!include[ISO/IEC 29500 URL](../includes/iso-iec-29500-link.md)]
specification describes the attributes of the `<sldLayout/>` element.


|  **Attributes**  | **Description**   |
| -----------------|-------------------|
|  matchingName (Matching Name) | Specifies a name to be used in place of the name attribute within the cSld element. This is used for layout matching in response to layout changes and template applications.  <br/><br/>The possible values for this attribute are defined by the W3C XML Schema `string` datatype. |
| preserve (Preserve Slide Layout) | Specifies whether the corresponding slide layout is deleted when all the slides that follow that layout are deleted. If this attribute is not specified then a value of `false` should be assumed by the generating application. This would mean that the slide would in fact be deleted if no slides within the presentation were related to it.<br/><br/>The possible values for this attribute are defined by the W3C XML Schema `boolean` datatype. |
| showMasterPhAnim (Show Master Placeholder Animations) | Specifies whether or not to display animations on placeholders from the master slide.<br/><br/>The possible values for this attribute are defined by the W3C XML Schema `boolean` datatype. |
| showMasterSp (Show Master Shapes) | Specifies if shapes on the master slide should be shown on slides or not.<br/><br/>The possible values for this attribute are defined by the W3C XML Schema `boolean` datatype. |
| type (Slide Layout Type) |                                                                                                                                          Specifies the slide layout type that is used by this slide.<br/><br/>The possible values for this attribute are defined by the ST_SlideLayoutType simple type (§19.7.15). |
| userDrawn (Is User Drawn) |  Specifies if the corresponding object has been drawn by the user and should thus not be deleted. This allows for the flagging of slides that contain user drawn data.<br/><br/>The possible values for this attribute are defined by the W3C XML Schema `boolean` datatype. |

## The Open XML SDK SlideLayout Class

The OXML SDK `SlideLayout` class represents
the `<sldLayout/>` element defined in the Open XML File Format schema for
PresentationML documents. Use the `SlideLayout` class to manipulate individual
`<sldLayout/>` elements in a PresentationML document.

Classes that represent child elements of the `<sldLayout/>` element and
that are therefore commonly associated with the `SlideLayout` class are shown in the following
list.

### ColorMapOverride Class

The `ColorMapOverride` class corresponds to
the `<clrMapOvr/>` element. The following information from the [!include[ISO/IEC 29500 URL](../includes/iso-iec-29500-link.md)]
specification introduces the `<clrMapOvr/>` element:

This element provides a mechanism with which to override the color
schemes listed within the `<ClrMap/>` element. If the
`<masterClrMapping/>` child element is present, the color scheme defined
by the master is used. If the `<overrideClrMapping/>` child element is
present, it defines a new color scheme specific to the parent notes
slide, presentation slide, or slide layout.

### CommonSlideData Class

The `CommonSlideData` class corresponds to
the `<cSld/>` element. The following information from the [!include[ISO/IEC 29500 URL](../includes/iso-iec-29500-link.md)]
specification introduces the `<cSld/>` element:

This element specifies a container for the type of slide information
that is relevant to all of the slide types. All slides share a common
set of properties that is independent of the slide type; the description
of these properties for any particular slide is stored within the
slide's `<cSld/>` container. Slide data specific to the slide type
indicated by the parent element is stored elsewhere.

The actual data in `<cSld/>` describe only the particular parent slide;
it is only the type of information stored that is common across all
slides.

### ExtensionListWithModification Class

The `ExtensionListWithModification` class
corresponds to the `<extLst/>`element. The following information from the
[!include[ISO/IEC 29500 URL](../includes/iso-iec-29500-link.md)]
specification introduces the `<extLst/>` element:

This element specifies the extension list with modification ability
within which all future extensions of element type `<ext/>` are defined.
The extension list along with corresponding future extensions is used to
extend the storage capabilities of the PresentationML framework. This
allows for various new kinds of data to be stored natively within the
framework.

> [!NOTE]
> Using this extLst element allows the generating application to store whether this extension property has been modified.

### HeaderFooter Class

The `HeaderFooter` class corresponds to the
`<hf/>` element. The following information from the [!include[ISO/IEC 29500 URL](../includes/iso-iec-29500-link.md)]
specification introduces the `<hf/>` element:

This element specifies the header and footer information for a slide.
Headers and footers consist of placeholders for text that should be
consistent across all slides and slide types, such as a date and time,
slide numbering, and custom header and footer text.

### Timing Class

The `Timing` class corresponds to the
`<timing/>` element. The following information from the [!include[ISO/IEC 29500 URL](../includes/iso-iec-29500-link.md)]
specification introduces the `<timing/>` element:

This element specifies the timing information for handling all
animations and timed events within the corresponding slide. This
information is tracked via time nodes within the `<timing/>` element.
More information on the specifics of these time nodes and how they are
to be defined can be found within the Animation section of the
PresentationML framework.

### Transition Class

The `Transition` class corresponds to the `<transition/>` element. The following information from the [!include[ISO/IEC 29500 URL](../includes/iso-iec-29500-link.md)] specification introduces the `<transition/>` element:

This element specifies the kind of slide transition that should be used to transition to the current slide from the previous slide. That is, the transition information is stored on the slide that appears after the transition is complete.

## Working with the SlideLayout Class

As shown in the Open XML SDK code sample that follows, every instance of
the `SlideLayout` class is associated with an
instance of the <xref:DocumentFormat.OpenXml.Packaging.SlideLayoutPart> class, which represents a
slide layout part, one of the required parts of a PresentationML
presentation file package. Each `SlideLayout`
class instance must also be associated with instances of the <xref:DocumentFormat.OpenXml.Presentation.SlideMaster> and <xref:DocumentFormat.OpenXml.Presentation.Slide> classes, which are in turn associated
with similarly named required presentation parts, represented by the
<xref:DocumentFormat.OpenXml.Packaging.SlideMasterPart> and <xref:DocumentFormat.OpenXml.Packaging.SlidePart> classes.

The `SlideLayout` class, which represents the
`<sldLayout/>` element, is therefore also associated with a series of
other classes that represent the child elements of the `<sldLayout/>`
element. Among these classes, as shown in the following code sample, are
the `CommonSlideData` class, the `ColorMapOverride` class, the <xref:DocumentFormat.OpenXml.Presentation.ShapeTree> class, and the <xref:DocumentFormat.OpenXml.Presentation.Shape> class.

## Open XML SDK Code Example

The following method from the article [How to: Create a presentation document by providing a file name](how-to-create-a-presentation-document-by-providing-a-file-name.md) adds a new slide layout part to an existing presentation and creates an instance of an Open XML SDK `SlideLayout` class in the new slide layout part. The `SlideLayout` class constructor creates instances of the `CommonSlideData` class and the `ColorMapOverride` class. The `CommonSlideData` class constructor creates an instance of the <xref:DocumentFormat.OpenXml.Presentation.ShapeTree> class, whose constructor in turn creates additional class instances: an instance of the <xref:DocumentFormat.OpenXml.Presentation.NonVisualGroupShapeProperties> class, an instance of the <xref:DocumentFormat.OpenXml.Presentation.GroupShapeProperties> class, and an instance of the <xref:DocumentFormat.OpenXml.Presentation.Shape> class.

The namespace represented by the letter *P* in the code is the <xref:DocumentFormat.OpenXml.Presentation> namespace.

### [C#](#tab/cs)
[!code-csharp[](../../samples/presentation/create_by_providing_a_file_name/cs/Program.cs#snippet100)]

### [Visual Basic](#tab/vb)
[!code-vb[](../../samples/presentation/create_by_providing_a_file_name/vb/Program.vb#snippet100)]
***

## Generated PresentationML

When the Open XML SDK code is run, the following XML is written to the PresentationML document file referenced in the code.

```xml
    <?xml version="1.0" encoding="utf-8"?>
    <p:sldLayout xmlns:p="http://schemas.openxmlformats.org/presentationml/2006/main">
      <p:cSld>
        <p:spTree>
          <p:nvGrpSpPr>
            <p:cNvPr id="1"
                     name="" />
            <p:cNvGrpSpPr />
            <p:nvPr />
          </p:nvGrpSpPr>
          <p:grpSpPr>
            <a:xfrm xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main" />
          </p:grpSpPr>
          <p:sp>
            <p:nvSpPr>
              <p:cNvPr id="2"
                       name="" />
              <p:cNvSpPr>
                <a:spLocks noGrp="1"
                           xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main" />
              </p:cNvSpPr>
              <p:nvPr>
                <p:ph />
              </p:nvPr>
            </p:nvSpPr>
            <p:spPr />
            <p:txBody>
              <a:bodyPr xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main" />
              <a:lstStyle xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main" />
              <a:p xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main">
                <a:endParaRPr />
              </a:p>
            </p:txBody>
          </p:sp>
        </p:spTree>
      </p:cSld>
      <p:clrMapOvr>
        <a:masterClrMapping xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main" />
      </p:clrMapOvr>
    </p:sldLayout>
```

## See also

[About the Open XML SDK for Office](../about-the-open-xml-sdk.md)

[How to: Create a Presentation by Providing a File Name](how-to-create-a-presentation-document-by-providing-a-file-name.md)

[How to: Apply a theme to a presentation](how-to-apply-a-theme-to-a-presentation.md)  
