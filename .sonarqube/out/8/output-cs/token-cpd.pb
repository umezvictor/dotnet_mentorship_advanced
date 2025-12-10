∫!
åC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\API\Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args, 0
)1 2
;2 3
Log 
. 
Logger 

= 
new 
LoggerConfiguration $
($ %
)% &
. 
ReadFrom 

.
 
Configuration 
( 
builder !
.! "
Configuration" /
)0 1
. 
CreateLogger 
( 
) 
; 
builder 
. 
Services 
.  
AddPresentationLayer %
(% &
builder' .
.. /
Configuration/ <
)= >
. !
AddBusinessLogicLayer 
( 
) 
. 
AddDataAccessLayer 
( 
builder 
. 
Configuration +
), -
;- .
builder 
. 
Services 
. 
AddControllers 
(  
)  !
;! "
builder 
. 
Services 
. #
AddEndpointsApiExplorer (
(( )
)) *
;* +
builder 
. 
Services 
. 
AddSwaggerGen 
( 
c  !
=>" $
{ 
c   
.   
IncludeXmlComments   
(   
Path   
.   
Combine   #
(  # $

AppContext  % /
.  / 0
BaseDirectory  0 =
,  = >
$"!! 
{!! 
Assembly!! 
.!!  
GetExecutingAssembly!! "
(!!" #
)!!# $
.!!$ %
GetName!!% ,
(!!, -
)!!- .
.!!. /
Name!!/ 3
}!!3 4
$str!!4 8
"!!8 9
)!!: ;
)!!< =
;!!= >
}"" 
)"" 
;"" 
builder$$ 
.$$ 
Services$$ 
.$$ 
	AddScoped$$ 
<$$ 
IRabbitMqClient$$ *
,$$* +
RabbitMqClient$$, :
>$$: ;
($$; <
)$$< =
;$$= >
builder%% 
.%% 
Host%% 
.%% 

UseSerilog%% 
(%% 
)%% 
;%% 
var'' 
app'' 
='' 	
builder''
 
.'' 
Build'' 
('' 
)'' 
;'' 
if(( 
((( 
app(( 
.(( 
Environment(( 
.(( 
IsDevelopment(( !
(((! "
)((" #
)((# $
{)) 
app** 
.** 

UseSwagger** 
(** 
)** 
;** 
app++ 
.++ 
UseSwaggerUI++ 
(++ 
)++ 
;++ 
},, 
app-- 
.-- 
UseMiddleware-- 
<-- (
AccessTokenLoggingMiddleware-- .
>--. /
(--/ 0
)--0 1
;--1 2
app// 
.// 
UseCors// 
(// 
AppConstants// 
.// 

CorsPolicy// $
)//% &
;//& '
app00 
.00 
UseHttpsRedirection00 
(00 
)00 
;00 
app22 
.22 
UseRateLimiter22 
(22 
)22 
;22 
app33 
.33 
UseAuthentication33 
(33 
)33 
;33 
app44 
.44 
UseAuthorization44 
(44 
)44 
;44 
app66 
.66 
MapControllers66 
(66 
)66 
;66 
await88 
app88 	
.88	 

RunAsync88
 
(88 
)88 
;88 
	namespace:: 	
CartService::
 
{;; 
public<< 
partial<< 
class<< 
Program<< 
;<< 
}== “'
¨C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\API\Middleware\AccessTokenLoggingMiddleware.cs
	namespace 	
API
 
. 

Middleware 
; 
public 
sealed 
class (
AccessTokenLoggingMiddleware 0
{ 
private 
readonly	 
RequestDelegate !
_next" '
;' (
private 
readonly	 
ILogger 
< (
AccessTokenLoggingMiddleware 6
>6 7
_logger8 ?
;? @
public

 (
AccessTokenLoggingMiddleware

 $
(

% &
RequestDelegate

& 5
next

6 :
,

: ;
ILogger

< C
<

C D(
AccessTokenLoggingMiddleware

D `
>

` a
logger

b h
)

h i
{ 
_next 
= 	
next
 
; 
_logger 	
=
 
logger 
; 
} 
public 
async 
Task 
Invoke 
( 
HttpContext &
context' .
). /
{ 
var 

authHeader 
= 
context 
. 
Request "
." #
Headers# *
[* +
$str+ :
]: ;
.; <
FirstOrDefault< J
(J K
)K L
;L M
if 
( 
! 
string 
. 
IsNullOrEmpty 
( 

authHeader '
)( )
&&* ,

authHeader- 7
.7 8

StartsWith8 B
(B C
$strD M
,M N
StringComparison 
. 
OrdinalIgnoreCase %
)& '
)' (
{ 
var 
token 
= 

authHeader 
. 
	Substring #
(# $
$str% .
.. /
Length/ 5
)6 7
.7 8
Trim8 <
(< =
)= >
;> ?
try 
{ 
var 
handler 
= 
new #
JwtSecurityTokenHandler -
(- .
). /
;/ 0
var 
jwt 
= 
handler 
. 
ReadJwtToken "
(" #
token$ )
)* +
;+ ,
var!! 
sub!! 
=!! 
jwt!! 
.!! 
Claims!! 
.!! 
FirstOrDefault!! '
(!!' (
c!!) *
=>!!+ -
c!!. /
.!!/ 0
Type!!0 4
==!!5 7
$str!!8 =
)!!> ?
?!!? @
.!!@ A
Value!!A F
;!!F G
var"" 
clientId"" 
="" 
jwt"" 
."" 
Claims"" 
."" 
FirstOrDefault"" ,
("", -
c"". /
=>""0 2
c""3 4
.""4 5
Type""5 9
=="": <
$str""= H
)""I J
?""J K
.""K L
Value""L Q
;""Q R
var## 
role## 
=## 
jwt## 
.## 
Claims## 
.## 
FirstOrDefault## (
(##( )
c##* +
=>##, .
c##/ 0
.##0 1
Type##1 5
==##6 8
$str##9 ?
||$$ 
c$$ 
.$$ 	
Type$$	 
==$$ 
$str$$ O
)$$P Q
?$$Q R
.$$R S
Value$$S X
;$$X Y
var%% 
permissions%% 
=%% 
jwt%% 
.%% 
Claims%%  
.%%  !
Where%%! &
(%%& '
c%%( )
=>%%* ,
c%%- .
.%%. /
Type%%/ 3
==%%4 6
$str%%7 C
)%%D E
.%%E F
Select%%F L
(%%L M
c%%N O
=>%%P R
c%%S T
.%%T U
Value%%U Z
)%%[ \
.%%\ ]
ToArray%%] d
(%%d e
)%%e f
;%%f g
_logger'' 
.'' 
LogInformation'' 
('' 
$str(( m
,((m n
sub)) 
,)) 	
clientId))
 
,)) 
role)) 
,)) 
string))  
.))  !
Join))! %
())% &
$str))' +
,))+ ,
permissions))- 8
)))9 :
)** 
;** 
}++ 
catch,, 
(,,	 

	Exception,,
 
ex,, 
),, 
{-- 
_logger.. 
... 
LogError.. 
(.. 
ex.. 
,.. 
$str.. >
)..? @
;..@ A
}// 
}00 
await22 
_next22 
(22 
context22 
)22 
;22 
}33 
}44  
ùC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\API\Jobs\RabbitMqListenerJob.cs
	namespace 	
API
 
. 
Jobs 
; 
[ '
DisallowConcurrentExecution 
] 
public 
class 
RabbitMqListenerJob  
:! "
IJob# '
{ 
private 
readonly	 
IRabbitMqClient !
_rabbitMqClient" 1
;1 2
private 
readonly	 
ICartService 
_cartService +
;+ ,
public 
RabbitMqListenerJob 
( 
IRabbitMqClient ,
rabbitMqClient- ;
,; <
ICartService= I
cartServiceJ U
)U V
{ 
_rabbitMqClient 
= 
rabbitMqClient "
;" #
_cartService 
= 
cartService 
; 
} 
public 
async 
Task 
Execute 
(  
IJobExecutionContext 0
context1 8
)8 9
{ 
var 
message 
= 
await 
_rabbitMqClient %
.% &
ConsumeMessageAsync& 9
(9 :
RabbitMQConstants; L
.L M
ProductQueueM Y
)Z [
;[ \
if 
( 
! 
string 
. 
IsNullOrEmpty 
( 
message $
)% &
)& '
{ "
ProductUpdatedContract 
? 
productUpdate (
=) *
null+ /
;/ 0
productUpdate 
= 
JsonSerializer !
.! "
Deserialize" -
<- ."
ProductUpdatedContract. D
>D E
(E F
messageG N
)O P
;P Q
if 
( 
productUpdate 
!= 
null 
) 
{ 
await 	
_cartService
 
. 3
'UpdateCartItemsFromMessageConsumerAsync >
(> ?
productUpdate@ M
,M N
CancellationTokenO `
.` a
Nonea e
)f g
;g h
}   
}"" 
}$$ 
}%% ‡
™C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\API\Infrastructure\GlobalExceptionHandler.cs
	namespace 	
API
 
. 
Infrastructure 
; 
internal 
sealed	 
class "
GlobalExceptionHandler ,
(- .
ILogger. 5
<5 6"
GlobalExceptionHandler6 L
>L M
loggerN T
,T U"
IProblemDetailsServiceV l"
problemDetailsService	m Ç
)
Ç É
: 
IExceptionHandler 
{ 
public		 
async		 
	ValueTask		 
<		 
bool		 
>		 
TryHandleAsync		 ,
(		- .
HttpContext

 
httpContext

 
,

 
	Exception 

	exception 
, 
CancellationToken 
cancellationToken $
)$ %
{ 
logger 
. 	
LogError	 
( 
	exception 
, 
$str 5
)6 7
;7 8
var 
problemDetails 
= 
new 
ProblemDetails )
{ 
Status 	
=
 
	exception 
switch 
{ 
ArgumentException 
=> 
StatusCodes $
.$ %
Status400BadRequest% 8
,8 9
_ 
=> 
StatusCodes	 
. (
Status500InternalServerError 1
} 
, 
Title 
=	 

$str 
, 
Type 
= 	
	exception
 
. 
GetType 
( 
) 
. 
Name "
," #
Detail 	
=
 
	exception 
. 
Message 
} 
; 
return 
await	 !
problemDetailsService $
.$ %
TryWriteAsync% 2
(2 3
new4 7!
ProblemDetailsContext8 M
{ 
	Exception 
= 
	exception 
, 
HttpContext 
= 
httpContext 
, 
ProblemDetails 
= 
problemDetails "
}   
)   
;   
}!! 
}"" ﬂa
òC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\API\DependencyInjection.cs
	namespace 	
API
 
; 
public 
static 
class 
DependencyInjection '
{ 
public 
static 
IServiceCollection ! 
AddPresentationLayer" 6
(7 8
this8 <
IServiceCollection= O
servicesP X
,X Y
IConfigurationZ h
configurationi v
)v w
=>x z
services 
. 
AddSecurity 
( 
configuration 
) 
. 
AddServices 
( 
configuration 
)  !
. 
AddQuartzSetup 
( 
) 
; 
public 
static 
IServiceCollection !
AddServices" -
(. /
this/ 3
IServiceCollection4 F
servicesG O
,O P
IConfigurationQ _
configuration` m
)m n
{ 
services 

.
 
AddRazorPages 
( 
) 
; 
services 

.
 
AddCors 
( 
o 
=> 
o 
. 
	AddPolicy $
($ %
AppConstants& 2
.2 3

CorsPolicy3 =
,= >
builder? F
=>G I
{ 
builder 

.
 
WithOrigins 
( 
$str 
) 
. 
AllowAnyOrigin ,
(, -
)- .
.	 

AllowAnyMethod
 
( 
) 
.	 

AllowAnyHeader
 
( 
) 
; 
} 
) 
) 
; 
services 

.
 
AddExceptionHandler 
< "
GlobalExceptionHandler 5
>5 6
(6 7
)7 8
;8 9
services!! 

.!!
 
AddProblemDetails!! 
(!! 
options!! %
=>!!& (
{"" 
options## 

.##
 #
CustomizeProblemDetails## "
=### $
context##% ,
=>##- /
{$$ 
context%% 
.%% 
ProblemDetails%% 
.%% 
Instance%% #
=%%$ %
$"&& 
{&& 
context&& 
.&& 
HttpContext&& 
.&& 
Request&& #
.&&# $
Method&&$ *
}&&* +
$str&&+ ,
{&&, -
context&&- 4
.&&4 5
HttpContext&&5 @
.&&@ A
Request&&A H
.&&H I
Path&&I M
}&&M N
"&&N O
;&&O P
context(( 
.(( 
ProblemDetails(( 
.(( 

Extensions(( %
.((% &
TryAdd((& ,
(((, -
$str((. 9
,((9 :
context((; B
.((B C
HttpContext((C N
.((N O
TraceIdentifier((O ^
)((_ `
;((` a
Activity** 
?** 
activity** 
=** 
context**  
.**  !
HttpContext**! ,
.**, -
Features**- 5
.**5 6
Get**6 9
<**9 : 
IHttpActivityFeature**: N
>**N O
(**O P
)**P Q
?**Q R
.**R S
Activity**S [
;**[ \
context++ 
.++ 
ProblemDetails++ 
.++ 

Extensions++ %
.++% &
TryAdd++& ,
(++, -
$str++. 7
,++7 8
activity++9 A
?++A B
.++B C
Id++C E
)++F G
;++G H
},, 
;,, 
}-- 
)-- 
;-- 
services// 

.//
 
AddRateLimiter// 
(// 
options// "
=>//# %
{00 
options11 

.11
 
RejectionStatusCode11 
=11  
StatusCodes11! ,
.11, -$
Status429TooManyRequests11- E
;11E F
options22 

.22
 
	AddPolicy22 
(22 
AppConstants22 "
.22" #
RateLimitingPolicy22# 5
,225 6
httpContext227 B
=>22C E
RateLimitPartition33 
.33 !
GetFixedWindowLimiter33 ,
(33, -
partitionKey44 
:44 
httpContext44 
.44 

Connection44 )
.44) *
RemoteIpAddress44* 9
?449 :
.44: ;
ToString44; C
(44C D
)44D E
,44E F
factory55 
:55 
_55 
=>55 
new55 )
FixedWindowRateLimiterOptions55 4
{66 
PermitLimit77 
=77 
$num77 
,77 
Window88 
=88 
TimeSpan88 
.88 
FromMinutes88 #
(88# $
$num88% &
)88' (
,88( )
}99 
)99 
)99	 

;99
 
}:: 
):: 
;:: 
services== 

.==
 
AddApiVersioning== 
(== 
options== $
=>==% '
{>> 
options?? 

.??
 /
#AssumeDefaultVersionWhenUnspecified?? .
=??/ 0
true??1 5
;??5 6
options@@ 

.@@
 
DefaultApiVersion@@ 
=@@ 
new@@ "
Asp@@# &
.@@& '

Versioning@@' 1
.@@1 2

ApiVersion@@2 <
(@@< =
$num@@> ?
,@@? @
$num@@A B
)@@C D
;@@D E
optionsAA 

.AA
 
ReportApiVersionsAA 
=AA 
trueAA #
;AA# $
}BB 
)BB 
.CC 
AddMvcCC 	
(CC	 

optionsCC 
=>CC 
{DD 
optionsEE 

.EE
 
ConventionsEE 
.EE 
AddEE 
(EE 
newEE (
VersionByNamespaceConventionEE  <
(EE< =
)EE= >
)EE? @
;EE@ A
}FF 
)FF 
.GG 
AddApiExplorerGG 
(GG 
optionsGG 
=>GG 
{HH 
optionsII 

.II
 
GroupNameFormatII 
=II 
$strII #
;II# $
optionsJJ 

.JJ
 %
SubstituteApiVersionInUrlJJ $
=JJ% &
trueJJ' +
;JJ+ ,
}LL 
)LL 
;LL 
returnMM 
servicesMM	 
;MM 
}NN 
publicPP 
staticPP 
IServiceCollectionPP !
AddSecurityPP" -
(PP. /
thisPP/ 3
IServiceCollectionPP4 F
servicesPPG O
,PPO P
IConfigurationPPQ _
configurationPP` m
)PPm n
{QQ 
servicesRR 

.RR
 
AddAuthenticationRR 
(RR 
$strRR &
)RR' (
.SS 
AddJwtBearerSS 
(SS 
$strSS 
,SS 
optionsSS %
=>SS& (
{TT 
optionsUU 
.UU 
	AuthorityUU 
=UU 
configurationUU '
[UU' (
$strUU( <
]UU< =
;UU= >
optionsWW 
.WW %
TokenValidationParametersWW '
=WW( )
newWW* -%
TokenValidationParametersWW. G
{XX 
ValidateAudienceYY 
=YY 
falseYY 
,YY  
RoleClaimTypeZZ 
=ZZ 
$strZZ U
,ZZU V
NameClaimType[[ 
=[[ 
$str[[ 
}]] 
;]] 
}^^ 
)^^ 
;^^ 	
services`` 

.``
 
AddAuthorization`` 
(`` 
options`` $
=>``% '
{aa 
optionsbb 

.bb
 
	AddPolicybb 
(bb 
$strbb  
,bb  !
policybb" (
=>bb) +
{cc 
policydd 

.dd
 $
RequireAuthenticatedUserdd #
(dd# $
)dd$ %
;dd% &
policyee 

.ee
 
RequireClaimee 
(ee 
$stree  
,ee  !
$stree" +
)ee, -
;ee- .
}ff 
)ff 
;ff 
optionsii 

.ii
 
	AddPolicyii 
(ii 
$strii /
,ii/ 0
policyii1 7
=>ii8 :
{jj 
policykk 

.kk
 
RequireAssertionkk 
(kk 
contextkk $
=>kk% '
(ll 
contextll 
.ll 
Userll 
.ll 
IsInRolell 
(ll 
$strll &
)ll' (
&&ll) +
contextmm 
.mm 
Usermm 
.mm 
HasClaimmm 
(mm 
AppConstantsmm *
.mm* +
PermissionClaimmm+ :
,mm: ;
$strmm< B
)mmC D
||mmE G
contextnn 
.nn 
Usernn 
.nn 
HasClaimnn 
(nn 
AppConstantsnn *
.nn* +
PermissionClaimnn+ :
,nn: ;
$strnn< D
)nnE F
||nnG I
contextoo 
.oo 
Useroo 
.oo 
HasClaimoo 
(oo 
AppConstantsoo *
.oo* +
PermissionClaimoo+ :
,oo: ;
$stroo< D
)ooE F
||ooG I
contextpp 
.pp 
Userpp 
.pp 
HasClaimpp 
(pp 
AppConstantspp *
.pp* +
PermissionClaimpp+ :
,pp: ;
$strpp< D
)ppE F
)qq 
||rr 
(ss 
contextss 
.ss 
Userss 
.ss 
IsInRoless 
(ss 
$strss ,
)ss- .
&&ss/ 1
contexttt 
.tt 
Usertt 
.tt 
HasClaimtt 
(tt 
AppConstantstt *
.tt* +
PermissionClaimtt+ :
,tt: ;
$strtt< B
)ttC D
)uu 
)vv 
;vv 
}ww 
)ww 
;ww 
}xx 
)xx 
;xx 
returnzz 
serviceszz	 
;zz 
}{{ 
public}} 
static}} 
IServiceCollection}} !
AddQuartzSetup}}" 0
(}}1 2
this}}2 6
IServiceCollection}}7 I
services}}J R
)}}R S
{~~ 
services 

.
 
	AddQuartz 
( 
	configure 
=>  "
{
ÄÄ 
var
ÇÇ 
jobKey
ÇÇ 
=
ÇÇ 
new
ÇÇ 
JobKey
ÇÇ 
(
ÇÇ 
nameof
ÇÇ "
(
ÇÇ" #!
RabbitMqListenerJob
ÇÇ$ 7
)
ÇÇ8 9
)
ÇÇ: ;
;
ÇÇ; <
	configure
ÉÉ 
.
ÑÑ 
AddJob
ÑÑ 
<
ÑÑ !
RabbitMqListenerJob
ÑÑ 
>
ÑÑ  
(
ÑÑ  !
jobKey
ÑÑ" (
)
ÑÑ) *
.
ÖÖ 

AddTrigger
ÖÖ 
(
ÖÖ 
trigger
ÜÜ 
=>
ÜÜ 
trigger
ÜÜ 
.
ÜÜ 
ForJob
ÜÜ 
(
ÜÜ 
jobKey
ÜÜ  &
)
ÜÜ' (
.
áá  
WithSimpleSchedule
áá 
(
áá 
schedule
àà 
=>
àà 
schedule
àà 
.
àà #
WithIntervalInSeconds
àà 0
(
àà0 1
$num
àà2 4
)
àà5 6
.
àà6 7
RepeatForever
àà7 D
(
ààD E
)
ààE F
)
ààG H
)
ààI J
;
ààJ K
}
ää 
)
ää 
;
ää 
services
åå 

.
åå
 $
AddQuartzHostedService
åå !
(
åå! "
options
åå# *
=>
åå+ -
{
çç 
options
éé 

.
éé
 #
WaitForJobsToComplete
éé  
=
éé! "
true
éé# '
;
éé' (
}
èè 
)
èè 
;
èè 
return
ëë 
services
ëë	 
;
ëë 
}
íí 
}ìì ®9
¢C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\API\Controllers\v2\CartController.cs
	namespace

 	
API


 
.

 
Controllers

 
.

 
v2

 
{ 
[ 
ApiController 
] 
[ 
Route 
( 
$str	 1
)2 3
]3 4
[ 

ApiVersion 
( 
$str 
) 
] 
[ 
	Authorize 
( !
AuthenticationSchemes "
=# $
$str% -
). /
]/ 0
public 
class 
CartController 
( 
ICartService *
cartService+ 6
)6 7
:8 9
ControllerBase: H
{ 
[ 
HttpPost 
] 
[  
ProducesResponseType 
( 
typeof 
(  
Response! )
<) *
string* 0
>0 1
)2 3
,3 4
StatusCodes5 @
.@ A
Status200OKA L
)M N
]N O
[  
ProducesResponseType 
( 
typeof 
(  
Response! )
<) *
string* 0
>0 1
)2 3
,3 4
StatusCodes5 @
.@ A
Status400BadRequestA T
)U V
]V W
[ '
ProducesDefaultResponseType 
] 
[ 
	Authorize 
( 
Policy 
= 
$str 0
)1 2
]2 3
public   
async  	 
Task   
<   
IActionResult   !
>  ! "
AddItemToCartV2  # 2
(  3 4
[  4 5
FromBody  5 =
]  = > 
AddItemToCartRequest  ? S
request  T [
,  [ \
CancellationToken  ] n
cancellationToken	  o Ä
)
  Ä Å
{!! 
if"" 
("" 
await"" 
cartService"" 
."" 
AddItemToCartAsync"" +
(""+ ,
request""- 4
,""4 5
cancellationToken""6 G
)""H I
)""I J
return## 

Ok## 
(## 
new## 
Response## 
<## 
string## "
>##" #
(### $
ResponseMessage##% 4
.##4 5
ItemAddedToCart##5 D
)##E F
)##G H
;##H I
return$$ 	

BadRequest$$
 
($$ 
new$$ 
Response$$ "
<$$" #
string$$# )
>$$) *
($$* +
ResponseMessage$$, ;
.$$; <
ItemNotAddedToCart$$< N
,$$N O
false$$P U
)$$V W
)$$X Y
;$$Y Z
}%% 
[// 

HttpDelete// 
]// 
[00  
ProducesResponseType00 
(00 
typeof00 
(00  
Response00! )
<00) *
string00* 0
>000 1
)002 3
,003 4
StatusCodes005 @
.00@ A
Status200OK00A L
)00M N
]00N O
[11  
ProducesResponseType11 
(11 
typeof11 
(11  
Response11! )
<11) *
string11* 0
>110 1
)112 3
,113 4
StatusCodes115 @
.11@ A
Status400BadRequest11A T
)11U V
]11V W
[22 '
ProducesDefaultResponseType22 
]22 
[33 
	Authorize33 
(33 
Policy33 
=33 
$str33 0
)331 2
]332 3
public44 
async44	 
Task44 
<44 
IActionResult44 !
>44! "
DeleteCartItemV244# 3
(444 5
[445 6
	FromRoute446 ?
]44? @
int44A D
id44E G
,44G H
[44I J
	FromRoute44J S
]44S T
string44U [
cartKey44\ c
,44c d
CancellationToken44e v
cancellationToken	44w à
)
44à â
{55 
if66 
(66 
await66 
cartService66 
.66 
DeleteCartItemAsync66 ,
(66, -
new66. 1%
DeleteItemFromCartRequest662 K
{66L M
Id66N P
=66Q R
id66S U
,66U V
CartKey66W ^
=66_ `
cartKey66a h
}66i j
,66j k
cancellationToken66l }
)66~ 
)	66 Ä
return77 

Ok77 
(77 
new77 
Response77 
<77 
string77 "
>77" #
(77# $
ResponseMessage77% 4
.774 5
ItemRemovedFromCart775 H
)77I J
)77K L
;77L M
return88 	

BadRequest88
 
(88 
new88 
Response88 "
<88" #
string88# )
>88) *
(88* +
ResponseMessage88, ;
.88; <
ItemNotRemoved88< J
)88K L
)88M N
;88N O
}99 
[BB 
HttpGetBB 

(BB
 
$strBB 
)BB 
]BB 
[CC  
ProducesResponseTypeCC 
(CC 
typeofCC 
(CC  
ResponseCC! )
<CC) *
ListCC* .
<CC. /
CartItemCC/ 7
>CC7 8
>CC8 9
)CC: ;
,CC; <
StatusCodesCC= H
.CCH I
Status200OKCCI T
)CCU V
]CCV W
[DD  
ProducesResponseTypeDD 
(DD 
StatusCodesDD $
.DD$ %
Status404NotFoundDD% 6
)DD7 8
]DD8 9
[EE '
ProducesDefaultResponseTypeEE 
]EE 
[FF 
	AuthorizeFF 
(FF 
PolicyFF 
=FF 
$strFF 0
)FF1 2
]FF2 3
publicGG 
asyncGG	 
TaskGG 
<GG 
IActionResultGG !
>GG! "
GetCartInfoV2GG# 0
(GG1 2
[GG2 3
	FromRouteGG3 <
]GG< =
stringGG> D
cartKeyGGE L
,GGL M
CancellationTokenGGN _
cancellationTokenGG` q
)GGq r
{HH 
varII 
cartII 
=II 
awaitII 
cartServiceII 
.II  
GetCartItemsAsyncII  1
(II1 2
cartKeyII3 :
,II: ;
cancellationTokenII< M
)IIN O
;IIO P
ifJJ 
(JJ 
cartJJ 
!=JJ 
nullJJ 
)JJ 
returnKK 

OkKK 
(KK 
newKK 
ResponseKK 
<KK 
ListKK  
<KK  !
CartItemKK! )
>KK) *
>KK* +
(KK+ ,
cartKK- 1
.KK1 2
	CartItemsKK2 ;
,KK; <
ResponseMessageKK= L
.KKL M
ItemsFetchedKKM Y
)KKZ [
)KK\ ]
;KK] ^
returnLL 	
NotFoundLL
 
(LL 
)LL 
;LL 
}MM 
}PP 
}QQ Û
¶C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\API\Controllers\v1\IdentityController.cs
	namespace 	
API
 
. 
Controllers 
. 
v1 
; 
[ 
Route 
( 
$str 
) 
] 
[ 
	Authorize 

]
 
public 
class 
IdentityController 
:  !
ControllerBase" 0
{		 
[

 
HttpGet

 	
]

	 

public 
IActionResult 
Get 
( 
) 
{ 
return 
new	 

JsonResult 
( 
from 
c 
in  "
User# '
.' (
Claims( .
select/ 5
new6 9
{: ;
c< =
.= >
Type> B
,B C
cD E
.E F
ValueF K
}L M
)N O
;O P
} 
} ö8
¢C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\API\Controllers\v1\CartController.cs
	namespace

 	
API


 
.

 
Controllers

 
.

 
v1

 
{ 
[ 
ApiController 
] 
[ 
Route 
( 
$str	 1
)2 3
]3 4
[ 

ApiVersion 
( 
$str 
) 
] 
[ 
	Authorize 
( !
AuthenticationSchemes "
=# $
$str% -
). /
]/ 0
public 
class 
CartController 
( 
ICartService *
cartService+ 6
)6 7
:8 9
ControllerBase: H
{ 
[ 
HttpPost 
] 
[  
ProducesResponseType 
( 
typeof 
(  
Response! )
<) *
string* 0
>0 1
)2 3
,3 4
StatusCodes5 @
.@ A
Status200OKA L
)M N
]N O
[  
ProducesResponseType 
( 
typeof 
(  
Response! )
<) *
string* 0
>0 1
)2 3
,3 4
StatusCodes5 @
.@ A
Status400BadRequestA T
)U V
]V W
[ '
ProducesDefaultResponseType 
] 
[   
	Authorize   
(   
Policy   
=   
$str   0
)  1 2
]  2 3
public!! 
async!!	 
Task!! 
<!! 
IActionResult!! !
>!!! "
AddItemToCartV1!!# 2
(!!3 4
[!!4 5
FromBody!!5 =
]!!= > 
AddItemToCartRequest!!? S
request!!T [
,!![ \
CancellationToken!!] n
cancellationToken	!!o Ä
)
!!Ä Å
{"" 
if## 
(## 
await## 
cartService## 
.## 
AddItemToCartAsync## +
(##+ ,
request##- 4
,##4 5
cancellationToken##6 G
)##H I
)##I J
return$$ 

Ok$$ 
($$ 
new$$ 
Response$$ 
<$$ 
string$$ "
>$$" #
($$# $
ResponseMessage$$% 4
.$$4 5
ItemAddedToCart$$5 D
)$$E F
)$$G H
;$$H I
return%% 	

BadRequest%%
 
(%% 
new%% 
Response%% "
<%%" #
string%%# )
>%%) *
(%%* +
ResponseMessage%%, ;
.%%; <
ItemNotAddedToCart%%< N
,%%N O
false%%P U
)%%V W
)%%X Y
;%%Y Z
}&& 
[00 

HttpDelete00 
]00 
[11  
ProducesResponseType11 
(11 
typeof11 
(11  
Response11! )
<11) *
string11* 0
>110 1
)112 3
,113 4
StatusCodes115 @
.11@ A
Status200OK11A L
)11M N
]11N O
[22  
ProducesResponseType22 
(22 
typeof22 
(22  
Response22! )
<22) *
string22* 0
>220 1
)222 3
,223 4
StatusCodes225 @
.22@ A
Status400BadRequest22A T
)22U V
]22V W
[33 '
ProducesDefaultResponseType33 
]33 
[44 
	Authorize44 
(44 
Policy44 
=44 
$str44 0
)441 2
]442 3
public55 
async55	 
Task55 
<55 
IActionResult55 !
>55! "
DeleteCartItemV155# 3
(554 5
[555 6
	FromRoute556 ?
]55? @
int55A D
id55E G
,55G H
[55I J
	FromRoute55J S
]55S T
string55U [
cartKey55\ c
,55c d
CancellationToken55e v
cancellationToken	55w à
)
55à â
{66 
if77 
(77 
await77 
cartService77 
.77 
DeleteCartItemAsync77 ,
(77, -
new77. 1%
DeleteItemFromCartRequest772 K
{77L M
Id77N P
=77Q R
id77S U
,77U V
CartKey77W ^
=77_ `
cartKey77a h
}77i j
,77j k
cancellationToken77l }
)77~ 
)	77 Ä
return88 

Ok88 
(88 
new88 
Response88 
<88 
string88 "
>88" #
(88# $
ResponseMessage88% 4
.884 5
ItemRemovedFromCart885 H
)88I J
)88K L
;88L M
return99 	

BadRequest99
 
(99 
new99 
Response99 "
<99" #
string99# )
>99) *
(99* +
ResponseMessage99, ;
.99; <
ItemNotRemoved99< J
)99K L
)99M N
;99N O
}:: 
[DD 
HttpGetDD 

(DD
 
$strDD 
)DD 
]DD 
[EE  
ProducesResponseTypeEE 
(EE 
typeofEE 
(EE  
ResponseEE! )
<EE) *
CartEE* .
>EE. /
)EE0 1
,EE1 2
StatusCodesEE3 >
.EE> ?
Status200OKEE? J
)EEK L
]EEL M
[FF  
ProducesResponseTypeFF 
(FF 
StatusCodesFF $
.FF$ %
Status404NotFoundFF% 6
)FF7 8
]FF8 9
[GG '
ProducesDefaultResponseTypeGG 
]GG 
[HH 
	AuthorizeHH 
(HH 
PolicyHH 
=HH 
$strHH 0
)HH1 2
]HH2 3
publicII 
asyncII	 
TaskII 
<II 
IActionResultII !
>II! "
GetCartInfoV1II# 0
(II1 2
[II2 3
	FromRouteII3 <
]II< =
stringII> D
cartKeyIIE L
,IIL M
CancellationTokenIIN _
cancellationTokenII` q
)IIq r
{JJ 
varKK 
cartKK 
=KK 
awaitKK 
cartServiceKK 
.KK  
GetCartItemsAsyncKK  1
(KK1 2
cartKeyKK3 :
,KK: ;
cancellationTokenKK< M
)KKN O
;KKO P
ifLL 
(LL 
cartLL 
!=LL 
nullLL 
)LL 
returnMM 

OkMM 
(MM 
newMM 
ResponseMM 
<MM 
CartMM  
>MM  !
(MM! "
cartMM# '
,MM' (
ResponseMessageMM) 8
.MM8 9
ItemsFetchedMM9 E
)MMF G
)MMH I
;MMI J
returnNN 	
NotFoundNN
 
(NN 
)NN 
;NN 
}OO 
}RR 
}SS 