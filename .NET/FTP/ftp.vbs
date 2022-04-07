
Dim fso
Dim sharefolder
Dim sharefilename
Dim localfolder
Dim extension
Dim ftpfile
Dim ftplocation

sharefoldername = "\\10.108.48.3\Projects" '\\Fileedc01\xreports$\Rogers\hr_agents_daily*
sharefilename = "Rogers_HR_Agents_Daily.csv"

localfoldername = "C:\TEMP"
localfilename = "rogers_tm_list.csv"

ftpfile = "ftp.txt"
ftplocation = "127.0.0.1"

If Not Right(sharefoldername, 1) = "\" Then sharefoldername = sharefoldername & "\"
If Not Right(localfoldername, 1) = "\" Then localfolder = localfolder & "\"

Set fso = CreateObject("Scripting.FileSystemObject")

If fso.FolderExists(localfoldername) Then

	'newest file
	Set folder = fso.GetFolder(localfoldername)

	For Each file In folder.Files

		extension = fso.GetExtensionName(file.Path)

		'If fNewest = "" Then
		'    Set fNewest = aFile
		'Else
		'    If fNewest.DateCreated < aFile.DateCreated Then
		'        Set fNewest = aFile
		'    End If
		'End If
		
		'Msgbox extension
		
	Next

Else
	fso.CreateFolder(localfoldername)
End If

'delete local file
If fso.FileExists(localfolder & localfilename) Then
	fso.DeleteFile localfolder & localfilename, true 
End If

'copy file from share
If fso.FileExists(sharefolder & sharefilename) Then
	fso.CopyFile sharefolder & sharefilename, localfolder & localfilename, true
	fso.DeleteFile sharefolder & sharefilename, true
End If

'FTP
Dim oShell, oExec
Set oShell = CreateObject("WScript.Shell")
Set oExec = oShell.Exec("ftp -s:" & ftpfile & " " & ftplocation)
