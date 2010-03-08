Imports System
Imports EnvDTE
Imports EnvDTE80
Imports EnvDTE90
Imports System.Diagnostics

Public Module GetInterfaceName

    Sub GetIName()
        Regions.GetIName()
    End Sub

    Public Class Regions

        Shared Sub GetIName()
            Try
                DTE.UndoContext.Open("OrgInterface")

                Dim ele As CodeElement
                Dim fcm As FileCodeModel = DTE.ActiveDocument.ProjectItem.FileCodeModel
                For Each ele In fcm.CodeElements
                    '�����ռ�
                    If (ele.Kind = vsCMElement.vsCMElementNamespace) Then
                        Dim cs As CodeElement
                        For Each cs In ele.Members
                            '��
                            If (cs.Kind = vsCMElement.vsCMElementClass) Then

                                Dim classEndPoint As EditPoint = cs.GetEndPoint.CreateEditPoint()
                                classEndPoint.StartOfLine()

                                Dim ifaces As String
                                Dim iface As CodeInterface
                                Dim i As Integer = 0
                                For Each iface In cs.ImplementedInterfaces

                                    Dim os As Integer = 9

                                    classEndPoint.Insert(vbCrLf & vbCrLf)
                                    classEndPoint.Insert("".PadLeft(os - 1) & "#region " & iface.Name & " members")
                                    classEndPoint.Insert(vbCrLf & vbCrLf)

                                    Dim ifaceRegion As String
                                    Dim regionEnd As Boolean = False
                                    '�ӿ�
                                    For Each member In iface.Members
                                        '�ӿڷ���
                                        If (member.Kind = vsCMElement.vsCMElementFunction) Then
                                            ifaceRegion = ""
                                            regionEnd = False

                                            '��ȡRegion���
                                            ifaceRegion = GetRegionDesc(member)
                                            regionEnd = GetRegionEnd(member)

                                            Dim methodSign = iface.Name & "_" & member.Name
                                            '�ӿڷ���ǩ��
                                            If (i > 0) Then
                                                methodSign = iface.Name & "_" & iface.Name & "." & member.Name
                                            End If

                                            '�ӿڷ�������
                                            Dim param As CodeParameter
                                            For Each param In member.Parameters
                                                methodSign &= "_" & param.Type.TypeKind.ToString & "_" & param.Name
                                            Next

                                            Dim im As CodeElement
                                            For Each im In cs.Members

                                                '�෽��
                                                If (im.Kind = vsCMElement.vsCMElementFunction) And im.DocComment <> "" Then
                                                    '�෽��ǩ��
                                                    Dim toMethodSign = iface.Name & "_" & im.Name

                                                    '�෽������
                                                    Dim toParam As CodeParameter
                                                    For Each toParam In im.Parameters
                                                        toMethodSign &= "_" & toParam.Type.TypeKind.ToString & "_" & toParam.Name
                                                    Next

                                                    'ǩ���Ƚ�
                                                    If methodSign = toMethodSign Then
                                                        '�˴��������е��෽��ע��
                                                        im.DocComment = "<doc></doc>"
                                                        Dim desc As String
                                                        desc = im.DocComment.ToString.Replace(Environment.NewLine, "")

                                                        '����regionע��ͷ
                                                        If (ifaceRegion <> "") Then
                                                            classEndPoint.Insert(ifaceRegion)
                                                            classEndPoint.Insert(vbCrLf)
                                                        End If

                                                        '�ƶ������嵽��β����ʹ���е�ʵ�ֽӿڷ������򱣳�һ��
                                                        Dim startPoint As TextPoint
                                                        Dim endPoint As TextPoint

                                                        startPoint = im.GetStartPoint()
                                                        endPoint = im.GetEndPoint()

                                                        Dim sep As EditPoint = startPoint.CreateEditPoint()
                                                        Dim eep As EditPoint = endPoint.CreateEditPoint()

                                                        sep.StartOfLine()
                                                        classEndPoint.Insert(sep.GetText(eep))
                                                        sep.Delete(eep)
                                                        classEndPoint.Insert(Chr(13) & Chr(13))


                                                        'ע��Ϊ�յ��ж�
                                                        If (desc = "<doc></doc>") Then
                                                            Dim docComment As String = member.DocComment
                                                            im.DocComment = docComment
                                                        End If

                                                        '����regionע��β
                                                        If (regionEnd) Then
                                                            Dim offset As Integer
                                                            Dim start As EditPoint = im.GetStartPoint().CreateEditPoint()
                                                            offset = start.LineCharOffset

                                                            classEndPoint.Insert(vbCrLf)
                                                            classEndPoint.Insert("".PadLeft(offset - 1))
                                                            classEndPoint.Insert("#endregion")
                                                            classEndPoint.Insert(vbCrLf & vbCrLf)
                                                        End If

                                                    End If
                                                End If
                                            Next
                                        End If
                                    Next

                                    classEndPoint.Insert("".PadLeft(os - 1) & "#endregion")
                                    classEndPoint.Insert(Chr(13))
                                    i += 1

                                Next
                            End If
                        Next
                    End If
                Next

                NoInterfaceMethod()

                Dim objTextDoc As TextDocument
                Dim objMovePt As EditPoint
                Dim objEditPt As EditPoint

                'TODO �˴�����Ҫ�������� ���������������
                '�滻��Region
                DeleteEmptyRegion()

                '�滻����Ŀ���
                DeleteMultiEmptyLines()

                '�滻��Region
                DeleteEmptyRegion()

                '�滻����Ŀ���
                DeleteMultiEmptyLines()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                DTE.UndoContext.Close()
            End Try

        End Sub
        '��ȡregionͷ����ʶ
        Private Shared Function GetRegionDesc(ByRef func As CodeFunction) As String
            Dim tpStart As TextPoint = func.GetStartPoint
            Dim tpEnd As TextPoint = func.GetStartPoint
            Dim epStart As EditPoint = tpStart.CreateEditPoint()
            Dim epEnd As EditPoint = tpEnd.CreateEditPoint()
            epStart.LineUp(1)
            epEnd.LineUp(1)
            epStart.StartOfLine()
            epEnd.EndOfLine()

            Dim text As String = epStart.GetText(epEnd)

            While (text.Trim() = "" Or text.Trim().StartsWith("//"))
                epStart.LineUp(1)
                epEnd.LineUp(1)
                epStart.StartOfLine()
                epEnd.EndOfLine()
                text = epStart.GetText(epEnd)
            End While

            If (text.Trim().StartsWith("#region")) Then
                Return text
            End If

            Return ""
        End Function
        '��ȡregionβ����ʶ
        Private Shared Function GetRegionEnd(ByRef func As CodeFunction) As String
            Dim tpStart As TextPoint = func.GetStartPoint
            Dim tpEnd As TextPoint = func.GetStartPoint
            Dim epStart As EditPoint = tpStart.CreateEditPoint()
            Dim epEnd As EditPoint = tpEnd.CreateEditPoint()
            epStart.LineDown(1)
            epEnd.LineDown(1)
            epStart.StartOfLine()
            epEnd.EndOfLine()

            Dim text As String = epStart.GetText(epEnd)

            While (text.Trim() = "")
                epStart.LineDown(1)
                epEnd.LineDown(1)
                epStart.StartOfLine()
                epEnd.EndOfLine()
                text = epStart.GetText(epEnd)
            End While

            If (text.Trim().StartsWith("#endregion")) Then
                Return True
            End If

            Return False
        End Function
        '�滻����Ŀ���
        Private Shared Sub DeleteMultiEmptyLines()
            Dim objTextDoc As TextDocument
            Dim objMovePt As EditPoint
            Dim objEditPt As EditPoint

            objTextDoc = DTE.ActiveDocument.Object("TextDocument")
            objMovePt = objTextDoc.EndPoint.CreateEditPoint
            objEditPt = objTextDoc.StartPoint.CreateEditPoint
            objEditPt.StartOfDocument()
            objMovePt.EndOfDocument()

            objEditPt.ReplacePattern(objMovePt, "\n[ \f\n\r\t\v]*\n[ \f\n\r\t\v]*\n+", vbNewLine, vsFindOptions.vsFindOptionsFromStart & vsFindOptions.vsFindOptionsRegularExpression)
        End Sub
        '�滻��Region
        Private Shared Sub DeleteEmptyRegion()
            Dim objTextDoc As TextDocument
            Dim objMovePt As EditPoint
            Dim objEditPt As EditPoint

            objTextDoc = DTE.ActiveDocument.Object("TextDocument")
            objMovePt = objTextDoc.EndPoint.CreateEditPoint
            objEditPt = objTextDoc.StartPoint.CreateEditPoint
            objEditPt.StartOfDocument()
            objMovePt.EndOfDocument()

            'MsgBox(objEditPt.FindPattern("\#region.*\n+[ \f\n\r\t\v]*\#endregion", vsFindOptions.vsFindOptionsFromStart & vsFindOptions.vsFindOptionsRegularExpression))
            objEditPt.ReplacePattern(objMovePt, "\#region.*\n+[ \f\n\r\t\v]*\#endregion", vbCrLf, vsFindOptions.vsFindOptionsFromStart & vsFindOptions.vsFindOptionsRegularExpression)
        End Sub
        '�ǽӿڷ�������
        Private Shared Sub NoInterfaceMethod()
            Dim ele As CodeElement
            Dim fcm As FileCodeModel = DTE.ActiveDocument.ProjectItem.FileCodeModel
            For Each ele In fcm.CodeElements
                '�����ռ�
                If (ele.Kind = vsCMElement.vsCMElementNamespace) Then
                    Dim cs As CodeElement
                    For Each cs In ele.Members
                        '��
                        If (cs.Kind = vsCMElement.vsCMElementClass) Then
                            Dim classEndPoint As EditPoint = cs.GetEndPoint.CreateEditPoint()
                            classEndPoint.StartOfLine()

                            Dim os As Integer = 9
                            classEndPoint.Insert(Chr(13))
                            classEndPoint.Insert("".PadLeft(os - 1) & "#region ˽�з���")
                            classEndPoint.Insert(vbCrLf & vbCrLf)

                            Dim im As CodeElement
                            For Each im In cs.Members

                                '�෽��
                                If (im.Kind = vsCMElement.vsCMElementFunction) Then
                                    Dim cf As CodeFunction = CType(im, CodeFunction)
                                    If (cf.Access = vsCMAccess.vsCMAccessPrivate) Then

                                        Dim docComment As String = im.DocComment
                                        im.DocComment = "<doc></doc>"

                                        '�ƶ������嵽��β����ʹ���е�ʵ�ֽӿڷ������򱣳�һ��
                                        Dim startPoint As TextPoint
                                        Dim endPoint As TextPoint
                                        startPoint = im.GetStartPoint()
                                        endPoint = im.GetEndPoint()
                                        Dim sep As EditPoint = startPoint.CreateEditPoint()
                                        Dim eep As EditPoint = endPoint.CreateEditPoint()
                                        sep.StartOfLine()
                                        classEndPoint.Insert(sep.GetText(eep))
                                        sep.Delete(eep)
                                        classEndPoint.Insert(Chr(13) & Chr(13))

                                        im.DocComment = docComment
                                    End If

                                End If
                            Next

                            classEndPoint.Insert("".PadLeft(os - 1) & "#endregion")
                            classEndPoint.Insert(Chr(13))
                        End If
                    Next
                End If
            Next
        End Sub
    End Class

End Module
