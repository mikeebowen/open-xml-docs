Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Presentation
Imports Drawing = DocumentFormat.OpenXml.Drawing

Public Module Program
    Public Sub Main(args As String())
        SlideHelpers.InsertNewSlide.InsertNewSlide(args(0), Integer.Parse(args(1)), args(2))
    End Sub
End Module

Namespace SlideHelpers
    Public Class InsertNewSlide
        ' <Snippet0>
        ' <Snippet10>
        ' Insert a slide into the specified presentation.
        Public Shared Sub InsertNewSlide(presentationFile As String, position As Integer, slideTitle As String)
            ' <Snippet1>
            ' Open the source document as read/write. 
            Using presentationDocument As PresentationDocument = PresentationDocument.Open(presentationFile, True)
                ' </Snippet1>
                ' Pass the source document and the position and title of the slide to be inserted to the next method.
                InsertNewSlide(presentationDocument, position, slideTitle)
            End Using
        End Sub
        ' </Snippet10>
        ' <Snippet11>
        ' Insert the specified slide into the presentation at the specified position.
        Public Shared Function InsertNewSlide(presentationDocument As PresentationDocument, position As Integer, slideTitle As String) As SlidePart
            Dim presentationPart As PresentationPart = presentationDocument.PresentationPart

            ' Verify that the presentation is not empty.
            If presentationPart Is Nothing Then
                Throw New InvalidOperationException("The presentation document is empty.")
            End If

            ' Declare and instantiate a new slide.
            Dim slide As New Slide(New CommonSlideData(New ShapeTree()))
            Dim drawingObjectId As UInteger = 1

            ' Construct the slide content.            
            ' Specify the non-visual properties of the new slide.
            Dim commonSlideData As CommonSlideData = If(slide.CommonSlideData, slide.AppendChild(New CommonSlideData()))
            Dim shapeTree As ShapeTree = If(commonSlideData.ShapeTree, commonSlideData.AppendChild(New ShapeTree()))
            Dim nonVisualProperties As NonVisualGroupShapeProperties = shapeTree.AppendChild(New NonVisualGroupShapeProperties())
            nonVisualProperties.NonVisualDrawingProperties = New NonVisualDrawingProperties() With {.Id = 1, .Name = ""}
            nonVisualProperties.NonVisualGroupShapeDrawingProperties = New NonVisualGroupShapeDrawingProperties()
            nonVisualProperties.ApplicationNonVisualDrawingProperties = New ApplicationNonVisualDrawingProperties()

            ' Specify the group shape properties of the new slide.
            shapeTree.AppendChild(New GroupShapeProperties())
            ' </Snippet11>
            ' <Snippet12>
            ' Declare and instantiate the title shape of the new slide.
            Dim titleShape As Shape = shapeTree.AppendChild(New Shape())

            drawingObjectId += 1

            ' Specify the required shape properties for the title shape. 
            titleShape.NonVisualShapeProperties = New NonVisualShapeProperties(
                New NonVisualDrawingProperties() With {.Id = drawingObjectId, .Name = "Title"},
                New NonVisualShapeDrawingProperties(New Drawing.ShapeLocks() With {.NoGrouping = True}),
                New ApplicationNonVisualDrawingProperties(New PlaceholderShape() With {.Type = PlaceholderValues.Title}))
            titleShape.ShapeProperties = New ShapeProperties()

            ' Specify the text of the title shape.
            titleShape.TextBody = New TextBody(New Drawing.BodyProperties(),
                    New Drawing.ListStyle(),
                    New Drawing.Paragraph(New Drawing.Run(New Drawing.Text() With {.Text = slideTitle})))
            ' </Snippet12>
            ' <Snippet13>
            ' Declare and instantiate the body shape of the new slide.
            Dim bodyShape As Shape = shapeTree.AppendChild(New Shape())
            drawingObjectId += 1

            ' Specify the required shape properties for the body shape.
            bodyShape.NonVisualShapeProperties = New NonVisualShapeProperties(New NonVisualDrawingProperties() With {.Id = drawingObjectId, .Name = "Content Placeholder"},
                    New NonVisualShapeDrawingProperties(New Drawing.ShapeLocks() With {.NoGrouping = True}),
                    New ApplicationNonVisualDrawingProperties(New PlaceholderShape() With {.Index = 1}))
            bodyShape.ShapeProperties = New ShapeProperties()

            ' Specify the text of the body shape.
            bodyShape.TextBody = New TextBody(New Drawing.BodyProperties(),
                    New Drawing.ListStyle(),
                    New Drawing.Paragraph())
            ' </Snippet13>
            ' <Snippet14>
            ' Create the slide part for the new slide.
            Dim slidePart As SlidePart = presentationPart.AddNewPart(Of SlidePart)()

            ' Assign the new slide to the new slide part
            slidePart.Slide = slide

            ' Modify the slide ID list in the presentation part.
            ' The slide ID list should not be null.
            Dim slideIdList As SlideIdList = presentationPart.Presentation.SlideIdList

            ' Find the highest slide ID in the current list.
            Dim maxSlideId As UInteger = 1
            Dim prevSlideId As SlideId = Nothing

            Dim slideIds As OpenXmlElementList = slideIdList?.ChildElements

            For Each slideId As SlideId In slideIds
                If slideId.Id IsNot Nothing AndAlso slideId.Id.Value > maxSlideId Then
                    maxSlideId = slideId.Id
                End If

                position -= 1
                If position = 0 Then
                    prevSlideId = slideId
                End If
            Next

            maxSlideId += 1

            ' Get the ID of the previous slide.
            Dim lastSlidePart As SlidePart

            If prevSlideId IsNot Nothing AndAlso prevSlideId.RelationshipId IsNot Nothing Then
                lastSlidePart = CType(presentationPart.GetPartById(prevSlideId.RelationshipId), SlidePart)
            Else
                Dim firstRelId As String = CType(slideIds(0), SlideId).RelationshipId
                ' If the first slide does not contain a relationship ID, throw an exception.
                If firstRelId Is Nothing Then
                    Throw New ArgumentNullException(NameOf(firstRelId))
                End If

                lastSlidePart = CType(presentationPart.GetPartById(firstRelId), SlidePart)
            End If

            ' Use the same slide layout as that of the previous slide.
            If lastSlidePart.SlideLayoutPart IsNot Nothing Then
                slidePart.AddPart(lastSlidePart.SlideLayoutPart)
            End If

            ' Insert the new slide into the slide list after the previous slide.
            Dim newSlideId As SlideId = slideIdList.InsertAfter(New SlideId(), prevSlideId)
            newSlideId.Id = maxSlideId
            newSlideId.RelationshipId = presentationPart.GetIdOfPart(slidePart)
            ' </Snippet14>

            Return slidePart
        End Function
        ' </Snippet0>
    End Class
End Namespace
