;;; ============================================================================
;;;   FILENAME: Generate PSPad.ahk
;;; ============================================================================
;;;   PSPad Syntax Generator Script
;;; ============================================================================
;;;   AUTHOR:  Scott Greenberg  
;;;   COMPANY: SG Technology
;;;   VERSION: 1.0.0, 02/08/2005 - 02/08/2005
;;;   WEBSITE: http://gogogadgetscott.info/
;;;
;;; ============================================================================
;;;   NOTE:
;;;    Derived from Rajat's PSPad Syntax Generator Script
;;; ============================================================================

SetBatchLines, -1     ; Speeds up file operations.
SetWorkingDir, ..\..  ; Set it to the Editors folder.

TargetFile = PSPad\AutoHotkey.ini
FileDelete, %TargetFile%

FileAppend, `; PSPad keyword syntax file for AutoHotkey`n, %TargetFile%
FileAppend, `; Auto generated by GoGoGadgetScott's PSPad Syntax Generator Script`n`n, %TargetFile%

FileAppend, [Settings] `n, %TargetFile%
FileAppend, Name=AutoHotkey `n, %TargetFile%
FileAppend, HTMLGroup=0 `n, %TargetFile%
FileAppend, FileType=*.ahk `n, %TargetFile%
FileAppend, CommentString=; `n, %TargetFile%
FileAppend, CComment=1 `n, %TargetFile%
FileAppend, BasComment=1 `n, %TargetFile%
FileAppend, SingleQuote=1 `n, %TargetFile%
FileAppend, DoubleQuote=1 `n, %TargetFile%
FileAppend, Preprocessors=1 `n, %TargetFile%
FileAppend, KeyWordChars=-_# `n, %TargetFile%
FileAppend, [KeyWords]`n, %TargetFile%


;this doesn't require fancy cmd names for human reading,
;it just requires names to be highlighted. so getting first name only

Loop, Read, Syntax\Commands.txt, %TargetFile%
{
	CurrCmd =
	FullCmd = %a_loopreadline%
	
	;directives don't have first comma but a first space
	;so whichever is first, take it as end of cmd name
	StringGetPos, cPos, a_loopreadline, `,
	StringGetPos, sPos, a_loopreadline, %A_Space%
	
	IfLess, sPos, %cPos%
		IfGreater, sPos, 0
			StringLeft, CurrCmd, a_loopreadline, %sPos%
	
	IfLess, cPos, %sPos%
		IfGreater, cPos, 0
			StringLeft, CurrCmd, a_loopreadline, %cPos%

	IfLess, cPos, %sPos%
		IfLess, cPos, 0
			StringLeft, CurrCmd, a_loopreadline, %sPos%

	IfLess, sPos, %cPos%
		IfLess, sPos, 0
			StringLeft, CurrCmd, a_loopreadline, %cPos%
			
	StringReplace, FullCmd, FullCmd, ``n, `n, a
	StringReplace, FullCmd, FullCmd, ``t, `t, a

	StringReplace, CurrCmd, CurrCmd, [,, a
	StringReplace, CurrCmd, CurrCmd, %a_space%,, a
	
	;For a directive that has no parameters
	IfEqual, CurrCmd,
		CurrCmd = %a_loopreadline%
	
	
	;this check removes duplicates for loop and if
	IfNotEqual, CurrCmd, %LastCmd%
		FileAppend, %CurrCmd%=`n
	
	LastCmd = %CurrCmd%
}


FileAppend, `n[ReservedWords]`n, %TargetFile%

;Adding keywords including the blank lines and comments
Loop, Read, Syntax\Keywords.txt, %TargetFile%
	FileAppend, %A_LoopReadLine%=`n

FileAppend, `n`n, %TargetFile%

FileAppend, `n[KeyWords2]`n, %TargetFile%

;same with variables
Loop, Read, Syntax\Variables.txt, %TargetFile%
	FileAppend, %A_LoopReadLine%=`n


FileAppend, `n[KeyWords3]`n, %TargetFile%

;keys are added with and without {}
Loop, Read, Syntax\Keys.txt, %TargetFile%
{
	FileAppend, %A_LoopReadLine%=`n
	IfEqual, A_LoopReadLine,, Continue
	
	;comment check
	StringReplace, check, A_LoopReadLine, %A_Space%,, A
	StringReplace, check, check, %A_Tab%,, A
	StringLeft, check, check, 1
	IfEqual, check, `;, Continue
	
	FileAppend, {%A_LoopReadLine%}=`n
}
