û
õC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\API\Services\LinkService.cs
	namespace 	
API
 
. 
Services 
; 
internal 
sealed	 
class 
LinkService !
(" #
LinkGenerator# 0
linkGenerator1 >
,> ? 
IHttpContextAccessor 
httpContextAccessor )
)) *
:+ ,
ILinkService- 9
{ 
public		 
Link		 
GenerateLinks		 
(		 
string		 "
endpointName		# /
,		/ 0
object		1 7
?		7 8
routeValues		9 D
,		D E
string		F L
rel		M P
,		P Q
string		R X
method		Y _
)		_ `
{

 
var 
httpContext 
= 
httpContextAccessor '
.' (
HttpContext( 3
;3 4
var 
href 

= 
httpContext 
!= 
null  
? 
linkGenerator 
. 
GetUriByName 
(  
httpContext! ,
,, -
endpointName. :
,: ;
routeValues< G
)H I
: 
null 	
;	 

return 
new	 
Link 
( 
href 
?? 
string !
.! "
Empty" '
,' (
rel) ,
,, -
method. 4
)5 6
;6 7
} 
} ∞
éC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\API\Program.cs
var

 
builder

 
=

 
WebApplication

 
.

 
CreateBuilder

 *
(

* +
args

, 0
)

1 2
;

2 3
var 
_config 
= 
new  
ConfigurationBuilder &
(& '
)' (
. 
AddJsonFile 
( 
$str $
)% &
. 
Build 

(
 
) 
; 
Log 
. 
Logger 

= 
new 
LoggerConfiguration $
($ %
)% &
. 
ReadFrom 

.
 
Configuration 
( 
builder !
.! "
Configuration" /
)0 1
. 
CreateLogger 
( 
) 
; 
builder 
. 
Services 
.  
AddPresentationLayer %
(% &
_config' .
)/ 0
. !
AddBusinessLogicLayer 
( 
) 
. 
AddDataAccessLayer 
( 
_config 
) 
;  
builder 
. 
Services 
. "
AddHttpContextAccessor '
(' (
)( )
;) *
builder 
. 
Services 
. 
	AddScoped 
< 
ILinkService '
,' (
LinkService) 4
>4 5
(5 6
)6 7
;7 8
builder 
. 
Services 
. 
AddControllers 
(  
)  !
;! "
builder 
. 
Services 
. #
AddEndpointsApiExplorer (
(( )
)) *
;* +
builder 
. 
Services 
. 
AddSwaggerGen 
( 
)  
;  !
builder 
. 
Services 
. 
	AddScoped 
< 
IRabbitMqClient *
,* +
RabbitMqClient, :
>: ;
(; <
)< =
;= >
builder 
. 
Host 
. 

UseSerilog 
( 
) 
; 
var   
app   
=   	
builder  
 
.   
Build   
(   
)   
;   
if"" 
("" 
app"" 
."" 
Environment"" 
."" 
IsDevelopment"" !
(""! "
)""" #
)""# $
{## 
app$$ 
.$$ 

UseSwagger$$ 
($$ 
)$$ 
;$$ 
app%% 
.%% 
UseSwaggerUI%% 
(%% 
)%% 
;%% 
}&& 
app(( 
.(( 
UseCors(( 
((( 
AppConstants(( 
.(( 

CorsPolicy(( $
)((% &
;((& '
app)) 
.)) 
UseHttpsRedirection)) 
()) 
))) 
;)) 
app** 
.** 
UseRateLimiter** 
(** 
)** 
;** 
app++ 
.++ 
UseAuthorization++ 
(++ 
)++ 
;++ 
app,, 
.,, 
MapControllers,, 
(,, 
),, 
;,, 
await-- 
app-- 	
.--	 

RunAsync--
 
(-- 
)-- 
;-- 
	namespace// 	
CatalogService//
 
{00 
public11 
partial11 
class11 
Program11 
{11 
}11  !
}22 Ò
†C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\API\Jobs\RabbitMQPublisherJob.cs
	namespace		 	
API		
 
.		 
Jobs		 
;		 
[ '
DisallowConcurrentExecution 
] 
public 
class  
RabbitMQPublisherJob !
:" #
IJob$ (
{ 
private 
readonly	 !
IApplicationDbContext '

_dbContext( 2
;2 3
private 
readonly	 
IRabbitMqClient !
_rabbitMqClient" 1
;1 2
public  
RabbitMQPublisherJob 
( !
IApplicationDbContext 3
	dbContext4 =
,= >
IRabbitMqClient? N
rabbitMqClientO ]
)] ^
{ 

_dbContext 
= 
	dbContext 
; 
_rabbitMqClient 
= 
rabbitMqClient "
;" #
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
var 
outboxMessages 
= 
await 

_dbContext '
.' (
Outbox( .
.. /
Where/ 4
(4 5
x6 7
=>8 :
x; <
.< =
IsProcessed= H
==I K
falseL Q
)R S
.S T
ToListAsyncT _
(_ `
)` a
;a b
if 
( 
outboxMessages 
. 
Count 
> 
$num 
) 
{ 
List 
< 
Outbox 
> #
processedOutboxMessages '
=( )
new* -
(- .
). /
;/ 0
foreach 

( 
var 
outboxMessage 
in  
outboxMessages! /
)/ 0
{ 
var   
productData   
=   
JsonSerializer   $
.  $ %
Deserialize  % 0
<  0 1"
ProductUpdatedContract  1 G
>  G H
(  H I
outboxMessage  J W
.  W X
Data  X \
)  ] ^
;  ^ _
if!! 
(!! 
productData!! 
!=!! 
null!! 
)!! 
{"" 
await## 

_rabbitMqClient## 
.## 
PublishMessageAsync## .
(##. /
new##0 3"
ProductUpdatedContract##4 J
{$$ 
Id%% 
=%%	 

productData%% 
.%% 
Id%% 
,%% 
Name&& 

=&& 
productData&& 
.&& 
Name&& 
,&& 
Price'' 
='' 
productData'' 
.'' 
Price'' 
}(( 
,(( 
RabbitMQConstants(( 
.(( 
ProductQueue(( &
)((' (
;((( )
})) 
outboxMessage** 
.** 
IsProcessed** 
=** 
true**  $
;**$ %#
processedOutboxMessages++ 
.++ 
Add++ 
(++  
outboxMessage++! .
)++/ 0
;++0 1
},, 

_dbContext.. 
... 
Outbox.. 
... 
UpdateRange..  
(..  !#
processedOutboxMessages.." 9
)..: ;
;..; <
await// 

_dbContext//	 
.// 
SaveChangesAsync// $
(//$ %
)//% &
;//& '
}00 
}22 
}33 ‚
¨C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\API\Infrastructure\GlobalExceptionHandler.cs
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
}"" “a
öC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\API\DependencyInjection.cs
	namespace

 	
API


 
;

 
public 
static 
class 
DependencyInjection '
{ 
public 
static 
IServiceCollection ! 
AddPresentationLayer" 6
(7 8
this8 <
IServiceCollection= O
servicesP X
,X Y
IConfigurationZ h
configurationi v
)v w
=>x z
services 
. 
AddSecurity 
( 
configuration  
)! "
. 
AddServices 
( 
configuration  
)! "
. 
AddQuartzSetup 
( 
) 
; 
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
;:: 
return<< 
services<<	 
;<< 
}== 
public?? 
static?? 
IServiceCollection?? !
AddSecurity??" -
(??. /
this??/ 3
IServiceCollection??4 F
services??G O
,??O P
IConfiguration??Q _
configuration??` m
)??m n
{@@ 
servicesAA 

.AA
 
AddAuthenticationAA 
(AA 
$strAA &
)AA' (
.BB 
AddJwtBearerBB 
(BB 
$strBB 
,BB 
optionsBB %
=>BB& (
{CC 
optionsDD 
.DD 
	AuthorityDD 
=DD 
configurationDD '
[DD' (
$strDD( <
]DD< =
;DD= >
optionsFF 
.FF %
TokenValidationParametersFF '
=FF( )
newFF* -%
TokenValidationParametersFF. G
{GG 
ValidateAudienceHH 
=HH 
falseHH 
,HH  
RoleClaimTypeII 
=II 
$strII U
,IIU V
NameClaimTypeJJ 
=JJ 
$strJJ 
}LL 
;LL 
}MM 
)MM 
;MM 	
servicesOO 

.OO
 
AddAuthorizationOO 
(OO 
optionsOO $
=>OO% '
{PP 
optionsQQ 

.QQ
 
	AddPolicyQQ 
(QQ 
$strQQ  
,QQ  !
policyQQ" (
=>QQ) +
{RR 
policySS 

.SS
 $
RequireAuthenticatedUserSS #
(SS# $
)SS$ %
;SS% &
policyTT 

.TT
 
RequireClaimTT 
(TT 
$strTT  
,TT  !
$strTT" +
)TT, -
;TT- .
}UU 
)UU 
;UU 
optionsXX 

.XX
 
	AddPolicyXX 
(XX 
$strXX %
,XX% &
policyXX' -
=>XX. 0
{YY 
policyZZ 

.ZZ
 
RequireAssertionZZ 
(ZZ 
contextZZ $
=>ZZ% '
([[ 
context[[ 
.[[ 
User[[ 
.[[ 
IsInRole[[ 
([[ 
$str[[ &
)[[' (
&&[[) +
context\\ 
.\\ 
User\\ 
.\\ 
HasClaim\\ 
(\\ 
AppConstants\\ *
.\\* +
PermissionClaim\\+ :
,\\: ;
$str\\< B
)\\C D
||\\E G
context]] 
.]] 
User]] 
.]] 
HasClaim]] 
(]] 
AppConstants]] *
.]]* +
PermissionClaim]]+ :
,]]: ;
$str]]< D
)]]E F
||]]G I
context^^ 
.^^ 
User^^ 
.^^ 
HasClaim^^ 
(^^ 
AppConstants^^ *
.^^* +
PermissionClaim^^+ :
,^^: ;
$str^^< D
)^^E F
||^^G I
context__ 
.__ 
User__ 
.__ 
HasClaim__ 
(__ 
AppConstants__ *
.__* +
PermissionClaim__+ :
,__: ;
$str__< D
)__E F
)`` 
)bb 
;bb 
}cc 
)cc 
;cc 
optionsee 

.ee
 
	AddPolicyee 
(ee 
$stree /
,ee/ 0
policyee1 7
=>ee8 :
{ff 
policygg 

.gg
 
RequireAssertiongg 
(gg 
contextgg $
=>gg% '
(hh 
contexthh 
.hh 
Userhh 
.hh 
IsInRolehh 
(hh 
$strhh &
)hh' (
&&hh) +
contextii 
.ii 
Userii 
.ii 
HasClaimii 
(ii 
AppConstantsii *
.ii* +
PermissionClaimii+ :
,ii: ;
$strii< B
)iiC D
||iiE G
contextjj 
.jj 
Userjj 
.jj 
HasClaimjj 
(jj 
AppConstantsjj *
.jj* +
PermissionClaimjj+ :
,jj: ;
$strjj< D
)jjE F
||jjG I
contextkk 
.kk 
Userkk 
.kk 
HasClaimkk 
(kk 
AppConstantskk *
.kk* +
PermissionClaimkk+ :
,kk: ;
$strkk< D
)kkE F
||kkG I
contextll 
.ll 
Userll 
.ll 
HasClaimll 
(ll 
AppConstantsll *
.ll* +
PermissionClaimll+ :
,ll: ;
$strll< D
)llE F
)mm 
||nn 
(oo 
contextoo 
.oo 
Useroo 
.oo 
IsInRoleoo 
(oo 
$stroo ,
)oo- .
&&oo/ 1
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
$strpp< B
)ppC D
)qq 
)rr 
;rr 
}ss 
)ss 
;ss 
}tt 
)tt 
;tt 
returnvv 
servicesvv	 
;vv 
}ww 
publicxx 
staticxx 
IServiceCollectionxx !
AddQuartzSetupxx" 0
(xx1 2
thisxx2 6
IServiceCollectionxx7 I
servicesxxJ R
)xxR S
{yy 
serviceszz 

.zz
 
	AddQuartzzz 
(zz 
	configurezz 
=>zz  "
{{{ 
var}} 
jobKey}} 
=}} 
new}} 
JobKey}} 
(}} 
nameof}} "
(}}" # 
RabbitMQPublisherJob}}$ 8
)}}9 :
)}}; <
;}}< =
	configure~~ 
. 
AddJob 
<  
RabbitMQPublisherJob  
>  !
(! "
jobKey# )
)* +
.
ÄÄ 

AddTrigger
ÄÄ 
(
ÄÄ 
trigger
ÅÅ 
=>
ÅÅ 
trigger
ÅÅ 
.
ÅÅ 
ForJob
ÅÅ 
(
ÅÅ 
jobKey
ÅÅ  &
)
ÅÅ' (
.
ÇÇ  
WithSimpleSchedule
ÇÇ 
(
ÇÇ 
schedule
ÉÉ 
=>
ÉÉ 
schedule
ÉÉ 
.
ÉÉ #
WithIntervalInSeconds
ÉÉ 0
(
ÉÉ0 1
$num
ÉÉ2 4
)
ÉÉ5 6
.
ÉÉ6 7
RepeatForever
ÉÉ7 D
(
ÉÉD E
)
ÉÉE F
)
ÉÉG H
)
ÉÉI J
;
ÉÉJ K
}
ÖÖ 
)
ÖÖ 
;
ÖÖ 
services
áá 

.
áá
 $
AddQuartzHostedService
áá !
(
áá! "
options
áá# *
=>
áá+ -
{
àà 
options
ââ 

.
ââ
 #
WaitForJobsToComplete
ââ  
=
ââ! "
true
ââ# '
;
ââ' (
}
ää 
)
ää 
;
ää 
return
åå 
services
åå	 
;
åå 
}
çç 
}éé ±S
•C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\API\Controllers\ProductsController.cs
	namespace		 	
API		
 
.		 
Controllers		 
;		 
[ 
Route 
( 
$str 
) 
] 
[ 
ApiController 
] 
[ 
EnableRateLimiting 
( 
AppConstants !
.! "
RateLimitingPolicy" 4
)5 6
]6 7
public 
class 
ProductsController 
(  !
IProductService! 0
productService1 ?
)? @
:A B
ControllerBaseC Q
{ 
[ 
HttpPost 

]
 
[  
ProducesResponseType 
( 
typeof 
( 
Response  (
<( )
long) -
>- .
)/ 0
,0 1
StatusCodes2 =
.= >
Status200OK> I
)J K
]K L
[  
ProducesResponseType 
( 
typeof 
( 
Response  (
<( )
long) -
>- .
)/ 0
,0 1
StatusCodes2 =
.= >
Status400BadRequest> Q
)R S
]S T
[ '
ProducesDefaultResponseType 
] 
[ 
	Authorize 
( 
Policy 
= 
$str %
)& '
]' (
public 
async 
Task 
< 
IActionResult  
>  !
Add" %
(& '
[' (
FromBody( 0
]0 1
AddProductRequest2 C
requestD K
,K L
CancellationTokenM ^
cancellationToken_ p
)p q
{ 
var 
response 
= 
await 
productService %
.% &
AddProductAsync& 5
(5 6
request7 >
,> ?
cancellationToken@ Q
)R S
;S T
if 
( 
response 
. 
	Succeeded 
) 
return 	
Ok
 
( 
response 
) 
; 
return 

BadRequest	 
( 
response 
) 
;  
} 
[   

HttpDelete   
(   
$str   
,   
Name   
=   
$str   ,
)  - .
]  . /
[!!  
ProducesResponseType!! 
(!! 
typeof!! 
(!! 
Response!!  (
<!!( )
string!!) /
>!!/ 0
)!!1 2
,!!2 3
StatusCodes!!4 ?
.!!? @
Status200OK!!@ K
)!!L M
]!!M N
[""  
ProducesResponseType"" 
("" 
typeof"" 
("" 
Response""  (
<""( )
string"") /
>""/ 0
)""1 2
,""2 3
StatusCodes""4 ?
.""? @
Status400BadRequest""@ S
)""T U
]""U V
[## '
ProducesDefaultResponseType## 
]## 
[$$ 
	Authorize$$ 
($$ 
Policy$$ 
=$$ 
$str$$ %
)$$& '
]$$' (
public%% 
async%% 
Task%% 
<%% 
IActionResult%%  
>%%  !
Delete%%" (
(%%) *
int%%* -
id%%. 0
,%%0 1
CancellationToken%%2 C
cancellationToken%%D U
)%%U V
{&& 
var'' 
response'' 
='' 
await'' 
productService'' %
.''% &
DeleteProductAsync''& 8
(''8 9
new'': = 
DeleteProductRequest''> R
{''S T
Id''U W
=''X Y
id''Z \
}''] ^
,''^ _
cancellationToken''` q
)''r s
;''s t
if(( 
((( 
response(( 
.(( 
	Succeeded(( 
)(( 
return)) 	
Ok))
 
()) 
response)) 
))) 
;)) 
return** 

BadRequest**	 
(** 
response** 
)** 
;**  
}++ 
[-- 
HttpGet-- 	
(--	 

$str-- 
,-- 
Name-- 
=-- 
$str-- &
)--' (
]--( )
[..  
ProducesResponseType.. 
(.. 
typeof.. 
(.. 
Response..  (
<..( )

ProductDto..) 3
>..3 4
)..5 6
,..6 7
StatusCodes..8 C
...C D
Status200OK..D O
)..P Q
]..Q R
[//  
ProducesResponseType// 
(// 
typeof// 
(// 
Response//  (
<//( )

ProductDto//) 3
>//3 4
)//5 6
,//6 7
StatusCodes//8 C
.//C D
Status404NotFound//D U
)//V W
]//W X
[00 '
ProducesDefaultResponseType00 
]00 
[11 
	Authorize11 
(11 
Policy11 
=11 
$str11 /
)110 1
]111 2
public22 
async22 
Task22 
<22 
IActionResult22  
>22  !
GetById22" )
(22* +
[22+ ,
	FromRoute22, 5
]225 6
long227 ;
id22< >
,22> ?
CancellationToken22@ Q
cancellationToken22R c
)22c d
{33 
var44 
response44 
=44 
await44 
productService44 %
.44% &
GetProductByIdAsync44& 9
(449 :
id44; =
,44= >
cancellationToken44? P
)44Q R
;44R S
if55 
(55 
response55 
.55 
	Succeeded55 
)55 
return66 	
Ok66
 
(66 
response66 
)66 
;66 
return77 
NotFound77	 
(77 
response77 
)77 
;77 
}88 
[;; 
HttpGet;; 	
];;	 

[<<  
ProducesResponseType<< 
(<< 
typeof<< 
(<< 
Response<<  (
<<<( )
PaginatedResponse<<) :
<<<: ;
List<<; ?
<<<? @

ProductDto<<@ J
><<J K
><<K L
><<L M
)<<N O
,<<O P
StatusCodes<<Q \
.<<\ ]
Status200OK<<] h
)<<i j
]<<j k
[==  
ProducesResponseType== 
(== 
typeof== 
(== 
Response==  (
<==( )
PaginatedResponse==) :
<==: ;
List==; ?
<==? @

ProductDto==@ J
>==J K
>==K L
>==L M
)==N O
,==O P
StatusCodes==Q \
.==\ ]
Status404NotFound==] n
)==o p
]==p q
[>> '
ProducesDefaultResponseType>> 
]>> 
[?? 
	Authorize?? 
(?? 
Policy?? 
=?? 
$str?? /
)??0 1
]??1 2
public@@ 
async@@ 
Task@@ 
<@@ 
IActionResult@@  
>@@  !#
GetProductsByCategoryId@@" 9
(@@: ;
[@@; <
	FromQuery@@< E
]@@E F
GetProductsQuery@@G W
query@@X ]
,@@] ^
CancellationToken@@_ p
cancellationToken	@@q Ç
)
@@Ç É
{AA 
varBB 
responseBB 
=BB 
awaitBB 
productServiceBB %
.BB% &(
GetProductsByCategoryIdAsyncBB& B
(BBB C
queryBBD I
,BBI J
cancellationTokenBBK \
)BB] ^
;BB^ _
ifCC 
(CC 
responseCC 
.CC 
	SucceededCC 
)CC 
returnDD 	
OkDD
 
(DD 
responseDD 
)DD 
;DD 
returnEE 
NotFoundEE	 
(EE 
responseEE 
)EE 
;EE 
}FF 
[II 
HttpPutII 	
(II	 

$strII 
,II 
NameII 
=II 
$strII )
)II* +
]II+ ,
[JJ  
ProducesResponseTypeJJ 
(JJ 
typeofJJ 
(JJ 
ResponseJJ  (
<JJ( )
stringJJ) /
>JJ/ 0
)JJ1 2
,JJ2 3
StatusCodesJJ4 ?
.JJ? @
Status200OKJJ@ K
)JJL M
]JJM N
[KK  
ProducesResponseTypeKK 
(KK 
typeofKK 
(KK 
ResponseKK  (
<KK( )
stringKK) /
>KK/ 0
)KK1 2
,KK2 3
StatusCodesKK4 ?
.KK? @
Status400BadRequestKK@ S
)KKT U
]KKU V
[LL '
ProducesDefaultResponseTypeLL 
]LL 
[MM 
	AuthorizeMM 
(MM 
PolicyMM 
=MM 
$strMM %
)MM& '
]MM' (
publicNN 
asyncNN 
TaskNN 
<NN 
IActionResultNN  
>NN  !
UpdateNN" (
(NN) *
[NN* +
	FromRouteNN+ 4
]NN4 5
intNN6 9
idNN: <
,NN< =
[NN> ?
FromBodyNN? G
]NNG H 
UpdateProductRequestNNI ]
requestNN^ e
,NNe f
CancellationTokenNNg x
cancellationToken	NNy ä
)
NNä ã
{OO 
requestPP 	
.PP	 

IdPP
 
=PP 
idPP 
;PP 
varQQ 
responseQQ 
=QQ 
awaitQQ 
productServiceQQ %
.QQ% &
UpdateProductAsyncQQ& 8
(QQ8 9
requestQQ: A
,QQA B
cancellationTokenQQC T
)QQU V
;QQV W
ifRR 
(RR 
responseRR 
.RR 
	SucceededRR 
)RR 
returnSS 	
OkSS
 
(SS 
responseSS 
)SS 
;SS 
returnTT 

BadRequestTT	 
(TT 
responseTT 
)TT 
;TT  
}UU 
}VV ñQ
ßC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\API\Controllers\CategoriesController.cs
	namespace		 	
API		
 
.		 
Controllers		 
;		 
[ 
Route 
( 
$str 
) 
] 
[ 
ApiController 
] 
[ 
EnableRateLimiting 
( 
AppConstants !
.! "
RateLimitingPolicy" 4
)5 6
]6 7
public 
class  
CategoriesController !
(" #
ICategoryService# 3
categoryService4 C
)C D
:E F
ControllerBaseG U
{ 
[ 
HttpPost 

]
 
[  
ProducesResponseType 
( 
typeof 
( 
Response  (
<( )
string) /
>/ 0
)1 2
,2 3
StatusCodes4 ?
.? @
Status200OK@ K
)L M
]M N
[  
ProducesResponseType 
( 
typeof 
( 
Response  (
<( )
string) /
>/ 0
)1 2
,2 3
StatusCodes4 ?
.? @
Status400BadRequest@ S
)T U
]U V
[ '
ProducesDefaultResponseType 
] 
[ 
	Authorize 
( 
Policy 
= 
$str %
)& '
]' (
public 
async 
Task 
< 
IActionResult  
>  !
Add" %
(& '
[' (
FromBody( 0
]0 1
AddCategoryRequest2 D
requestE L
,L M
CancellationTokenN _
cancellationToken` q
)q r
{ 
var 
response 
= 
await 
categoryService &
.& '
AddCategoryAsync' 7
(7 8
request9 @
,@ A
cancellationTokenB S
)T U
;U V
if 
( 
response 
. 
	Succeeded 
) 
return 	
Ok
 
( 
response 
) 
; 
return 

BadRequest	 
( 
response 
) 
;  
} 
[!! 

HttpDelete!! 
(!! 
$str!! 
,!! 
Name!! 
=!! 
$str!! -
)!!. /
]!!/ 0
[""  
ProducesResponseType"" 
("" 
typeof"" 
("" 
Response""  (
<""( )
string"") /
>""/ 0
)""1 2
,""2 3
StatusCodes""4 ?
.""? @
Status200OK""@ K
)""L M
]""M N
[##  
ProducesResponseType## 
(## 
typeof## 
(## 
Response##  (
<##( )
string##) /
>##/ 0
)##1 2
,##2 3
StatusCodes##4 ?
.##? @
Status400BadRequest##@ S
)##T U
]##U V
[$$ '
ProducesDefaultResponseType$$ 
]$$ 
[%% 
	Authorize%% 
(%% 
Policy%% 
=%% 
$str%% %
)%%& '
]%%' (
public&& 
async&& 
Task&& 
<&& 
IActionResult&&  
>&&  !
Delete&&" (
(&&) *
[&&* +
	FromRoute&&+ 4
]&&4 5
int&&6 9
id&&: <
,&&< =
CancellationToken&&> O
cancellationToken&&P a
)&&a b
{'' 
var)) 
response)) 
=)) 
await)) 
categoryService)) &
.))& '
DeleteCategoryAsync))' :
()): ;
new))< ?!
DeleteCategoryRequest))@ U
{))V W
Id))X Z
=))[ \
id))] _
}))` a
,))a b
cancellationToken))c t
)))u v
;))v w
if** 
(** 
response** 
.** 
	Succeeded** 
)** 
return++ 	
Ok++
 
(++ 
response++ 
)++ 
;++ 
return,, 

BadRequest,,	 
(,, 
response,, 
),, 
;,,  
}.. 
[00 
HttpGet00 	
(00	 

$str00 
,00 
Name00 
=00 
$str00 '
)00( )
]00) *
[11  
ProducesResponseType11 
(11 
typeof11 
(11 
Response11  (
<11( )
CategoryDto11) 4
>114 5
)116 7
,117 8
StatusCodes119 D
.11D E
Status200OK11E P
)11Q R
]11R S
[22  
ProducesResponseType22 
(22 
typeof22 
(22 
Response22  (
<22( )
CategoryDto22) 4
>224 5
)226 7
,227 8
StatusCodes229 D
.22D E
Status404NotFound22E V
)22W X
]22X Y
[33 '
ProducesDefaultResponseType33 
]33 
[44 
	Authorize44 
(44 
Policy44 
=44 
$str44 /
)440 1
]441 2
public55 
async55 
Task55 
<55 
IActionResult55  
>55  !
GetById55" )
(55* +
int55+ .
id55/ 1
,551 2
CancellationToken553 D
cancellationToken55E V
)55V W
{66 
var77 
response77 
=77 
await77 
categoryService77 &
.77& '
GetCategoryById77' 6
(776 7
id778 :
,77: ;
cancellationToken77< M
)77N O
;77O P
if88 
(88 
response88 
.88 
	Succeeded88 
)88 
return99 	
Ok99
 
(99 
response99 
)99 
;99 
return:: 
NotFound::	 
(:: 
response:: 
):: 
;:: 
};; 
[>> 
HttpGet>> 	
]>>	 

[??  
ProducesResponseType?? 
(?? 
typeof?? 
(?? 
Response??  (
<??( )
List??) -
<??- .
CategoryDto??. 9
>??9 :
>??: ;
)??< =
,??= >
StatusCodes??? J
.??J K
Status200OK??K V
)??W X
]??X Y
[@@  
ProducesResponseType@@ 
(@@ 
typeof@@ 
(@@ 
Response@@  (
<@@( )
List@@) -
<@@- .
CategoryDto@@. 9
>@@9 :
>@@: ;
)@@< =
,@@= >
StatusCodes@@? J
.@@J K
Status404NotFound@@K \
)@@] ^
]@@^ _
[AA '
ProducesDefaultResponseTypeAA 
]AA 
[BB 
	AuthorizeBB 
(BB 
PolicyBB 
=BB 
$strBB /
)BB0 1
]BB1 2
publicCC 
asyncCC 
TaskCC 
<CC 
IActionResultCC  
>CC  !
GetAllCC" (
(CC) *
CancellationTokenCC* ;
cancellationTokenCC< M
)CCM N
{DD 
varEE 
responseEE 
=EE 
awaitEE 
categoryServiceEE &
.EE& '
GetCategoriesAsyncEE' 9
(EE9 :
cancellationTokenEE; L
)EEM N
;EEN O
ifFF 
(FF 
responseFF 
.FF 
	SucceededFF 
)FF 
returnGG 	
OkGG
 
(GG 
responseGG 
)GG 
;GG 
returnHH 
NotFoundHH	 
(HH 
responseHH 
)HH 
;HH 
}II 
[LL 
HttpPutLL 	
(LL	 

$strLL 
,LL 
NameLL 
=LL 
$strLL *
)LL+ ,
]LL, -
[MM  
ProducesResponseTypeMM 
(MM 
typeofMM 
(MM 
ResponseMM  (
<MM( )
stringMM) /
>MM/ 0
)MM1 2
,MM2 3
StatusCodesMM4 ?
.MM? @
Status200OKMM@ K
)MML M
]MMM N
[NN  
ProducesResponseTypeNN 
(NN 
typeofNN 
(NN 
ResponseNN  (
<NN( )
stringNN) /
>NN/ 0
)NN1 2
,NN2 3
StatusCodesNN4 ?
.NN? @
Status400BadRequestNN@ S
)NNT U
]NNU V
[OO '
ProducesDefaultResponseTypeOO 
]OO 
[PP 
	AuthorizePP 
(PP 
PolicyPP 
=PP 
$strPP %
)PP& '
]PP' (
publicQQ 
asyncQQ 
TaskQQ 
<QQ 
IActionResultQQ  
>QQ  !
UpdateQQ" (
(QQ) *
[QQ* +
	FromRouteQQ+ 4
]QQ4 5
intQQ6 9
idQQ: <
,QQ< =
[QQ> ?
FromBodyQQ? G
]QQG H!
UpdateCategoryRequestQQI ^
requestQQ_ f
,QQf g
CancellationTokenQQh y
cancellationToken	QQz ã
)
QQã å
{RR 
requestSS 	
.SS	 

IdSS
 
=SS 
idSS 
;SS 
varTT 
responseTT 
=TT 
awaitTT 
categoryServiceTT &
.TT& '
UpdateCategoryAsyncTT' :
(TT: ;
requestTT< C
,TTC D
cancellationTokenTTE V
)TTW X
;TTX Y
ifUU 
(UU 
responseUU 
.UU 
	SucceededUU 
)UU 
returnVV 	
OkVV
 
(VV 
responseVV 
)VV 
;VV 
returnWW 

BadRequestWW	 
(WW 
responseWW 
)WW 
;WW  
}XX 
}[[ 