rem sc \\bicmsdevedc01 stop W3SVC
rem sc \\bicmsdevedc02 stop W3SVC


set option=METRIC
REM  CPL  CVPASS   METRIC  FINANCE LOB  EMPLOYEE CONTACT
REM ****************************************************

REM xcopy \\wws10-001-stage\C$\Web\RefMapping\scripts\scripts.js  \\bicmsdevedc01\D$\Web\ReferenceMapping\scripts /Y
REM xcopy \\wws10-001-stage\C$\Web\RefMapping\scripts\scripts.js  \\bicmsdevedc02\D$\Web\ReferenceMapping\scripts /Y


REM xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\DataMap*.dll \\bicmsdevedc01\D$\Web\ReferenceMapping\bin /Y
REM xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\DataMap*.dll \\bicmsdevedc02\D$\web\ReferenceMapping\bin /Y


IF %option% == ALL ( GOTO CPL )
GOTO %option%

:CPL

xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\CPL.dll \\bicmsdevedc01\D$\Web\ReferenceMapping\bin /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\CPL.dll \\bicmsdevedc02\D$\web\ReferenceMapping\bin /Y

xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucConfigurationOverview.ascx  \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucConfigurationOverview.ascx  \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y

xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucFilter.ascx  \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucFilter.ascx  \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y

xcopy \\wws10-001-stage\C$\Web\RefMapping\PopUps\CPL\*.*  \\bicmsdevedc01\D$\Web\ReferenceMapping\Popups\CPL  /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\PopUps\CPL\*.*  \\bicmsdevedc02\D$\web\ReferenceMapping\PopUps\CPL  /Y
IF %option% == CPL  ( GOTO END )


:CVPASS
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\CVPass.dll \\bicmsdevedc01\D$\Web\ReferenceMapping\bin /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\CVPass.dll \\bicmsdevedc02\D$\web\ReferenceMapping\bin /Y

REM xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucCVPassRate_Approve.ascx  \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
REM xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucCVPassRate_Approve.ascx  \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y

REM xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucCVPassRate_View.ascx  \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
REM xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucCVPassRate_View.ascx  \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y

REM xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucCVPassRate_Filter.ascx  \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
REM xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucCVPassRate_Filter.ascx  \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y
IF %option% == CVPASS  ( GOTO END )


:METRIC
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\Metric.dll \\bicmsdevedc01\D$\Web\ReferenceMapping\bin /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\Metric.dll \\bicmsdevedc02\D$\web\ReferenceMapping\bin /Y

REM xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucMetricConfiguration.ascx  \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
REM xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucMetricConfiguration.ascx  \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y

REM xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucMetricName.ascx  \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
REM xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucMetricName.ascx  \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y

REM xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucMetricInsert.ascx  \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
REM xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucMetricInsert.ascx  \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y

IF %option% == METRIC  ( GOTO END )


:FINANCE
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\FinancialQUeue.dll \\bicmsdevedc01\D$\Web\ReferenceMapping\bin /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\FinancialQUeue.dll \\bicmsdevedc02\D$\web\ReferenceMapping\bin /Y

xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucFinancialQueue_Filter.ascx  \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucFinancialQueue_Filter.ascx  \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y

xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucFinancialQueue.ascx   \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucFinancialQueue.ascx   \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y

IF %option% == FINANCE  ( GOTO END )


:LOB
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\SchedulingLOB.dll \\bicmsdevedc01\D$\Web\ReferenceMapping\bin /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\SchedulingLOB.dll \\bicmsdevedc02\D$\web\ReferenceMapping\bin /Y

xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\DepartmentToLOB.dll \\bicmsdevedc01\D$\Web\ReferenceMapping\bin /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\DepartmentToLOB.dll \\bicmsdevedc02\D$\web\ReferenceMapping\bin /Y

xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucCVPassRate_Filter.ascx  \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucCVPassRate_Filter.ascx  \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y

xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\LOBEditor.ascx  \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\LOBEditor.ascx  \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y

xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\LOBScheduleMap.ascx   \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\LOBScheduleMap.ascx   \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y

xcopy \\wws10-001-stage\C$\Web\RefMapping\PopUps\Sched2LOB\*.*  \\bicmsdevedc01\D$\Web\ReferenceMapping\Popups\Sched2LOB  /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\PopUps\Sched2LOB\*.*  \\bicmsdevedc02\D$\web\ReferenceMapping\PopUps\Sched2LOB  /Y

IF %option% == LOB  ( GOTO END )


:EMPLOYEE
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\EmployeeIdMgmt.dll \\bicmsdevedc01\D$\Web\ReferenceMapping\bin /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\EmployeeIdMgmt.dll \\bicmsdevedc02\D$\web\ReferenceMapping\bin /Y

xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\EmployeeIDMgmt.ascx  \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\EmployeeIDMgmt.ascx  \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y

IF %option% == EMPLOYEE  ( GOTO END )


:CONTACT
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\ContactQueue.dll \\bicmsdevedc01\D$\Web\ReferenceMapping\bin /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\bin\ContactQueue.dll \\bicmsdevedc02\D$\web\ReferenceMapping\bin /Y

xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucContactQueue.ascx   \\bicmsdevedc01\D$\Web\ReferenceMapping\usercontrols  /Y
xcopy \\wws10-001-stage\C$\Web\RefMapping\usercontrols\ucContactQueue.ascx   \\bicmsdevedc02\D$\web\ReferenceMapping\usercontrols  /Y



:END 
REM sc \\bicmsdevedc01 start W3SVC
REM sc \\bicmsdevedc02 start W3SVC  