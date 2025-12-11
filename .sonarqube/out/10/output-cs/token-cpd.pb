Øa
ûC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\BLL\Services\ProductService.cs
	namespace 	
BLL
 
. 
Services 
; 
public 
sealed 
class 
ProductService "
(# $
IProductRepository$ 6
productRepository7 H
,H I
IMapperJ Q
mapperR X
,X Y
ILinkServiceZ f
linkServiceg r
,r s!
IApplicationDbContext 
	dbContext  
)  !
:" #
IProductService$ 3
{ 
public 
async 
Task 
< 
Response 
< 
int 
>  
>  !
AddProductAsync" 1
(2 3
AddProductRequest3 D
requestE L
,L M
CancellationTokenN _
cancellationToken` q
)q r
{ 
var 
createdProductId 
= 
await 
productRepository 0
.0 1
CreateAsync1 <
(< =
mapper> D
.D E
MapE H
<H I
ProductI P
>P Q
(Q R
requestS Z
)[ \
,\ ]
cancellationToken^ o
)p q
;q r
return 
new	 
Response 
< 
int 
> 
( 
createdProductId ,
,, -
ResponseMessage. =
.= >
ProductAdded> J
)K L
;L M
} 
public 
async 
Task 
< 
Response 
< 
string "
>" #
># $
DeleteProductAsync% 7
(8 9 
DeleteProductRequest9 M
requestN U
,U V
CancellationTokenW h
cancellationTokeni z
)z {
{ 
var 
product 
= 
await 
productRepository '
.' (
GetByIdAsync( 4
(4 5
request6 =
.= >
Id> @
,@ A
cancellationTokenB S
)T U
;U V
if 
( 
product 
== 
null 
) 
{ 
return 	
new
 
Response 
< 
string 
> 
( 
ResponseMessage  /
./ 0
ProductNotFound0 ?
,? @
falseA F
)G H
;H I
} 
await 
productRepository 
. 
DeleteAsync %
(% &
product' .
,. /
cancellationToken0 A
)B C
;C D
return 
new	 
Response 
< 
string 
> 
( 
ResponseMessage .
.. /
ProductDeleted/ =
)> ?
;? @
}   
public## 
async## 
Task## 
<## 
Response## 
<## 
PaginatedResponse## -
<##- .
List##. 2
<##2 3

ProductDto##3 =
>##= >
>##> ?
>##? @
>##@ A(
GetProductsByCategoryIdAsync##B ^
(##_ `
GetProductsQuery##` p
query##q v
,##v w
CancellationToken	##x â
cancellationToken
##ä õ
)
##õ ú
{$$ 
var%% 
paginatedResponse%% 
=%% 
await%% 
productRepository%%  1
.%%1 2(
GetProductsByCategoryIdAsync%%2 N
(&& 
query&& 

.&&
 

CategoryId&& 
,&& 
query&& 
.&& 

PageNumber&& '
,&&' (
query&&) .
.&&. /
PageSize&&/ 7
,&&7 8
cancellationToken&&9 J
)&&K L
;&&L M
if(( 
((( 
paginatedResponse(( 
is(( 
null(( 
&&((  "
!((# $
paginatedResponse(($ 5
!((5 6
.((6 7
Data((7 ;
!((; <
.((< =
Any((= @
(((@ A
)((A B
)((B C
return)) 	
new))
 
Response)) 
<)) 
PaginatedResponse)) (
<))( )
List))) -
<))- .

ProductDto)). 8
>))8 9
>))9 :
>)): ;
()); <
ResponseMessage))= L
.))L M
NotItemsPresent))M \
,))\ ]
false))^ c
)))d e
;))e f
return++ 
new++	 
Response++ 
<++ 
PaginatedResponse++ '
<++' (
List++( ,
<++, -

ProductDto++- 7
>++7 8
>++8 9
>++9 :
(++: ;
paginatedResponse++< M
,++M N
ResponseMessage++O ^
.++^ _
Success++_ f
)++g h
;++h i
},, 
public.. 
async.. 
Task.. 
<.. 
Response.. 
<.. 
string.. "
>.." #
>..# $
UpdateProductAsync..% 7
(..8 9 
UpdateProductRequest..9 M
request..N U
,..U V
CancellationToken..W h
cancellationToken..i z
)..z {
{// 
var00 
product00 
=00 
await00 
productRepository00 '
.00' (
GetByIdAsync00( 4
(004 5
request006 =
.00= >
Id00> @
,00@ A
cancellationToken00B S
)00T U
;00U V
if11 
(11 
product11 
is11 
null11 
)11 
{22 
return33 	
new33
 
Response33 
<33 
string33 
>33 
(33 
ResponseMessage33  /
.33/ 0
ProductNotFound330 ?
,33? @
false33A F
)33G H
;33H I
}44 
using66 
var66 
transaction66 
=66 
await66 
	dbContext66  )
.66) *
Database66* 2
.662 3!
BeginTransactionAsync663 H
(66H I
)66I J
;66J K
try88 
{99 
var:: 
updatedProduct:: 
=:: 
await:: 
productRepository:: /
.::/ 0
UpdateAsync::0 ;
(::; <
mapper::= C
.::C D
Map::D G
(::G H
request::I P
,::P Q
product::R Y
)::Z [
,::[ \
cancellationToken::] n
)::o p
;::p q
if;; 
(;; 
updatedProduct;; 
!=;; 
null;; 
);; 
{<< 
await>> 	
	dbContext>>
 
.>> 
Outbox>> 
.>> 
AddAsync>> #
(>># $
new>>% (
Outbox>>) /
{?? 
CreatedOnUTCAA 
=AA 
DateTimeAA 
.AA 
UtcNowAA #
,AA# $
DataBB 	
=BB
 
JsonSerializerBB 
.BB 
	SerializeBB $
(BB$ %
newBB& )"
ProductUpdatedContractBB* @
{CC 
IdDD 
=DD	 

updatedProductDD 
.DD 
IdDD 
,DD 
NameEE 

=EE 
updatedProductEE 
.EE 
NameEE  
,EE  !
PriceFF 
=FF 
updatedProductFF 
.FF 
PriceFF "
,FF" #
}GG 
)GG 
,GG 	
IsProcessedHH 
=HH 
falseHH 
}II 
,II 
cancellationTokenII 
)II 
;II 
awaitJJ 	
	dbContextJJ
 
.JJ 
SaveChangesAsyncJJ $
(JJ$ %
cancellationTokenJJ& 7
)JJ8 9
;JJ9 :
awaitKK 	
transactionKK
 
.KK 
CommitAsyncKK !
(KK! "
cancellationTokenKK# 4
)KK5 6
;KK6 7
returnLL 

newLL 
ResponseLL 
<LL 
stringLL 
>LL 
(LL  
ResponseMessageLL! 0
.LL0 1
ProductUpdatedLL1 ?
)LL@ A
;LLA B
}MM 
}NN 
catchOO 
{PP 
awaitQQ 
transactionQQ	 
.QQ 
RollbackAsyncQQ "
(QQ" #
cancellationTokenQQ$ 5
)QQ6 7
;QQ7 8
returnRR 	
newRR
 
ResponseRR 
<RR 
stringRR 
>RR 
(RR 
ResponseMessageRR  /
.RR/ 0
FailureRR0 7
)RR8 9
;RR9 :
}SS 
returnTT 
newTT	 
ResponseTT 
<TT 
stringTT 
>TT 
(TT 
ResponseMessageTT .
.TT. /
FailureTT/ 6
)TT7 8
;TT8 9
}UU 
publicYY 
asyncYY 
TaskYY 
<YY 
ResponseYY 
<YY 

ProductDtoYY &
>YY& '
>YY' (
GetProductByIdAsyncYY) <
(YY= >
longYY> B
idYYC E
,YYE F
CancellationTokenYYG X
cancellationTokenYYY j
)YYj k
{ZZ 
var[[ 
product[[ 
=[[ 
await[[ 
productRepository[[ '
.[[' (
GetByIdAsync[[( 4
([[4 5
id[[6 8
,[[8 9
cancellationToken[[: K
)[[L M
;[[M N
if\\ 
(\\ 
product\\ 
is\\ 
null\\ 
)\\ 
return]] 	
new]]
 
Response]] 
<]] 

ProductDto]] !
>]]! "
(]]" #
ResponseMessage]]$ 3
.]]3 4
ProductNotFound]]4 C
,]]C D
false]]E J
)]]K L
;]]L M

ProductDto__ 

productDto__ 
=__ 
mapper__  
.__  !
Map__! $
<__$ %

ProductDto__% /
>__/ 0
(__0 1
product__2 9
)__: ;
;__; <
AddLinksForProduct`` 
(`` 

productDto``  
)``! "
;``" #
returnaa 
newaa	 
Responseaa 
<aa 

ProductDtoaa  
>aa  !
(aa! "

productDtoaa# -
,aa- .
ResponseMessageaa/ >
.aa> ?
Successaa? F
)aaG H
;aaH I
}bb 
privatedd 
voiddd	 
AddLinksForProductdd  
(dd! "

ProductDtodd" ,

productDtodd- 7
)dd7 8
{ee 

productDtoff 
.ff 
Linksff 
.ff 
Addff 
(ff 
linkServiceff #
.ff# $
GenerateLinksff$ 1
(ff1 2
$strgg 
,gg 
newhh 	
{hh
 
idhh 
=hh 

productDtohh 
.hh 
Idhh 
}hh  
,hh  !
$strii 
,ii 
$strjj 
)jj 
)jj 
;jj 

productDtoll 
.ll 
Linksll 
.ll 
Addll 
(ll 
linkServicell #
.ll# $
GenerateLinksll$ 1
(ll1 2
$strmm 
,mm 
newnn 
{nn	 

idnn 
=nn 

productDtonn 
.nn 
Idnn 
}nn 
,nn  
$stroo 
,oo 
$strpp 
)pp 
)pp 
;pp 

productDtorr 
.rr 
Linksrr 
.rr 
Addrr 
(rr 
linkServicerr #
.rr# $
GenerateLinksrr$ 1
(rr1 2
$strss 
,ss 
newtt 
{tt	 

idtt 
=tt 

productDtott 
.tt 
Idtt 
}tt 
,tt  
$struu 
,uu 
$strvv 

)vv 
)vv 
;vv 
}xx 
}yy ™
üC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\BLL\Services\IProductService.cs
	namespace 	
BLL
 
. 
Services 
; 
public 
	interface 
IProductService  
{ 
Task 
< 
Response 
< 
int 
> 
> 
AddProductAsync $
(% &
AddProductRequest& 7
request8 ?
,? @
CancellationTokenA R
cancellationTokenS d
)d e
;e f
Task 
< 
Response 
< 
string 
> 
> 
DeleteProductAsync *
(+ , 
DeleteProductRequest, @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
;n o
Task		 
<		 
Response		 
<		 

ProductDto		 
>		 
>		 
GetProductByIdAsync		 /
(		0 1
long		1 5
id		6 8
,		8 9
CancellationToken		: K
cancellationToken		L ]
)		] ^
;		^ _
Task

 
<

 
Response

 
<

 
PaginatedResponse

  
<

  !
List

! %
<

% &

ProductDto

& 0
>

0 1
>

1 2
>

2 3
>

3 4(
GetProductsByCategoryIdAsync

5 Q
(

R S
GetProductsQuery

S c
query

d i
,

i j
CancellationToken

k |
cancellationToken	

} é
)


é è
;


è ê
Task 
< 
Response 
< 
string 
> 
> 
UpdateProductAsync *
(+ , 
UpdateProductRequest, @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
;n o
} ¶
†C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\BLL\Services\ICategoryService.cs
	namespace 	
BLL
 
. 
Services 
; 
public 
	interface 
ICategoryService !
{ 
Task 
< 
Response 
< 
string 
> 
> 
AddCategoryAsync (
() *
AddCategoryRequest* <
request= D
,D E
CancellationTokenF W
cancellationTokenX i
)i j
;j k
Task 
< 
Response 
< 
string 
> 
> 
DeleteCategoryAsync +
(, -!
DeleteCategoryRequest- B
requestC J
,J K
CancellationTokenL ]
cancellationToken^ o
)o p
;p q
Task		 
<		 
Response		 
<		 
List		 
<		 
CategoryDto		 
>		  
>		  !
>		! "
GetCategoriesAsync		# 5
(		6 7
CancellationToken		7 H
cancellationToken		I Z
)		Z [
;		[ \
Task

 
<

 
Response

 
<

 
CategoryDto

 
>

 
>

 
GetCategoryById

 ,
(

- .
int

. 1
id

2 4
,

4 5
CancellationToken

6 G
cancellationToken

H Y
)

Y Z
;

Z [
Task 
< 
Response 
< 
string 
> 
> 
UpdateCategoryAsync +
(, -!
UpdateCategoryRequest- B
requestC J
,J K
CancellationTokenL ]
cancellationToken^ o
)o p
;p q
} ƒA
üC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\BLL\Services\CategoryService.cs
	namespace 	
BLL
 
. 
Services 
; 
public		 
sealed		 
class		 
CategoryService		 #
(		$ %
ICategoryRepository		% 8
categoryRepository		9 K
,		K L
IMapper		M T
mapper		U [
,		[ \
ILinkService		] i
linkService		j u
)		u v
:		w x
ICategoryService			y â
{

 
public 
async 
Task 
< 
Response 
< 
string "
>" #
># $
AddCategoryAsync% 5
(6 7
AddCategoryRequest7 I
requestJ Q
,Q R
CancellationTokenS d
cancellationTokene v
)v w
{ 
var 
category 
= 
mapper 
. 
Map 
< 
Category $
>$ %
(% &
request' .
)/ 0
;0 1
await 
categoryRepository 
. 
CreateAsync &
(& '
category( 0
,0 1
cancellationToken2 C
)D E
;E F
return 
new	 
Response 
< 
string 
> 
( 
ResponseMessage .
.. /
CategoryAdded/ <
)= >
;> ?
} 
public 
async 
Task 
< 
Response 
< 
string "
>" #
># $
DeleteCategoryAsync% 8
(9 :!
DeleteCategoryRequest: O
requestP W
,W X
CancellationTokenY j
cancellationTokenk |
)| }
{ 
await 
categoryRepository 
. 
DeleteAsync &
(& '
request( /
./ 0
Id0 2
,2 3
cancellationToken4 E
)F G
;G H
return 
new	 
Response 
< 
string 
> 
( 
ResponseMessage .
.. /
CategoryDeleted/ >
)? @
;@ A
} 
public 
async 
Task 
< 
Response 
< 
List  
<  !
CategoryDto! ,
>, -
>- .
>. /
GetCategoriesAsync0 B
(C D
CancellationTokenD U
cancellationTokenV g
)g h
{ 
var 

categories 
= 
await 
categoryRepository +
.+ ,!
GetAllCategoriesAsync, A
(A B
cancellationTokenC T
)U V
;V W
return 
new	 
Response 
< 
List 
< 
CategoryDto &
>& '
>' (
(( )
mapper* 0
.0 1
Map1 4
<4 5
List5 9
<9 :
CategoryDto: E
>E F
>F G
(G H

categoriesI S
)T U
,U V
ResponseMessageW f
.f g
Successg n
)o p
;p q
} 
public 
async 
Task 
< 
Response 
< 
string "
>" #
># $
UpdateCategoryAsync% 8
(9 :!
UpdateCategoryRequest: O
requestP W
,W X
CancellationTokenY j
cancellationTokenk |
)| }
{   
var!! 
category!! 
=!! 
await!! 
categoryRepository!! )
.!!) *
GetByIdAsync!!* 6
(!!6 7
request!!8 ?
.!!? @
Id!!@ B
,!!B C
cancellationToken!!D U
)!!V W
;!!W X
if"" 
("" 
category"" 
is"" 
null"" 
)"" 
{## 
return$$ 	
new$$
 
Response$$ 
<$$ 
string$$ 
>$$ 
($$ 
ResponseMessage$$  /
.$$/ 0
CategoryNotFound$$0 @
,$$@ A
false$$B G
)$$H I
;$$I J
}%% 
await'' 
categoryRepository'' 
.'' 
UpdateAsync'' &
(''& '
mapper''( .
.''. /
Map''/ 2
(''2 3
request''4 ;
,''; <
category''= E
)''F G
,''G H
cancellationToken''I Z
)''[ \
;''\ ]
return(( 
new((	 
Response(( 
<(( 
string(( 
>(( 
((( 
ResponseMessage(( .
.((. /
CategoryUpdated((/ >
)((? @
;((@ A
})) 
public++ 
async++ 
Task++ 
<++ 
Response++ 
<++ 
CategoryDto++ '
>++' (
>++( )
GetCategoryById++* 9
(++: ;
int++; >
id++? A
,++A B
CancellationToken++C T
cancellationToken++U f
)++f g
{,, 
var.. 
category.. 
=.. 
await.. 
categoryRepository.. )
...) *
GetByIdAsync..* 6
(..6 7
id..8 :
,..: ;
cancellationToken..< M
)..N O
;..O P
if// 
(// 
category// 
is// 
null// 
)// 
return00 	
new00
 
Response00 
<00 
CategoryDto00 "
>00" #
(00# $
ResponseMessage00% 4
.004 5
CategoryNotFound005 E
,00E F
false00G L
)00M N
;00N O
CategoryDto22 
categoryDto22 
=22 
mapper22 "
.22" #
Map22# &
<22& '
CategoryDto22' 2
>222 3
(223 4
category225 =
)22> ?
;22? @
AddLinksForCategory33 
(33 
categoryDto33 "
)33# $
;33$ %
return44 
new44	 
Response44 
<44 
CategoryDto44 !
>44! "
(44" #
categoryDto44$ /
,44/ 0
ResponseMessage441 @
.44@ A
Success44A H
)44I J
;44J K
}55 
private77 
void77	 
AddLinksForCategory77 !
(77" #
CategoryDto77# .
categoryDto77/ :
)77: ;
{88 
categoryDto99 
.99 
Links99 
.99 
Add99 
(99 
linkService99 $
.99$ %
GenerateLinks99% 2
(992 3
$str:: 
,:: 
new;; 	
{;;
 
id;; 
=;; 
categoryDto;; 
.;; 
Id;; 
};;  !
,;;! "
$str<< 
,<< 
$str== 
)== 
)== 
;== 
categoryDto?? 
.?? 
Links?? 
.?? 
Add?? 
(?? 
linkService?? $
.??$ %
GenerateLinks??% 2
(??2 3
$str@@ 
,@@ 
newAA 
{AA	 

idAA 
=AA 
categoryDtoAA 
.AA 
IdAA 
}AA  
,AA  !
$strBB 
,BB 
$strCC 
)CC 
)CC 
;CC 
categoryDtoEE 
.EE 
LinksEE 
.EE 
AddEE 
(EE 
linkServiceEE $
.EE$ %
GenerateLinksEE% 2
(EE2 3
$strFF 
,FF 
newGG 
{GG	 

idGG 
=GG 
categoryDtoGG 
.GG 
IdGG 
}GG  
,GG  !
$strHH 
,HH 
$strII 

)II 
)II 
;II 
}KK 
}LL ï
ûC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\BLL\Mappings\ProductProfile.cs
	namespace 	
BLL
 
. 
Mappings 
; 
public 
class 
ProductProfile 
: 
Profile %
{ 
public 
ProductProfile 
( 
) 
{		 
	CreateMap

 
<

 
Product

 
,

 

ProductDto

 
>

  
(

  !
)

! "
.

" #

ReverseMap

# -
(

- .
)

. /
;

/ 0
	CreateMap 
< 
AddProductRequest 
, 
Product &
>& '
(' (
)( )
;) *
	CreateMap 
<  
UpdateProductRequest  
,  !
Product" )
>) *
(* +
)+ ,
. 
ForAllMembers 
( 
opts 
=> 
opts 

.
 
	Condition 
( 
( 
src 
, 
dest  
,  !
	srcMember" +
)+ ,
=>- /
	srcMember0 9
!=: <
null= A
)B C
)D E
;E F
} 
} õ
ûC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\BLL\Mappings\CatalogProfile.cs
	namespace 	
BLL
 
. 
Mappings 
; 
public 
class 
CatalogProfile 
: 
Profile %
{ 
public 
CatalogProfile 
( 
) 
{		 
	CreateMap

 
<

 
Category

 
,

 
CategoryDto

 !
>

! "
(

" #
)

# $
.

$ %

ReverseMap

% /
(

/ 0
)

0 1
;

1 2
	CreateMap 
< 
AddCategoryRequest 
, 
Category  (
>( )
() *
)* +
;+ ,
	CreateMap 
< !
UpdateCategoryRequest !
,! "
Category# +
>+ ,
(, -
)- .
. 
ForAllMembers 
( 
opts 
=> 
opts 

.
 
	Condition 
( 
( 
src 
, 
dest  
,  !
	srcMember" +
)+ ,
=>- /
	srcMember0 9
!=: <
null= A
)B C
)D E
;E F
} 
} ƒ
öC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\BLL\DependencyInjection.cs
	namespace 	
BLL
 
; 
public 
static 
class 
DependencyInjection '
{ 
public 
static 
IServiceCollection !!
AddBusinessLogicLayer" 7
(8 9
this9 =
IServiceCollection> P
servicesQ Y
)Y Z
{		 
return

 
services

	 
. 
AddPackages 
( 
) 
; 
} 
public 
static 
IServiceCollection !
AddPackages" -
(. /
this/ 3
IServiceCollection4 F
servicesG O
)O P
{ 
services 

.
 
AddAutoMapper 
( 
Assembly "
." # 
GetExecutingAssembly# 7
(7 8
)8 9
): ;
;; <
services 

.
 
	AddScoped 
< 
ICategoryService %
,% &
CategoryService' 6
>6 7
(7 8
)8 9
;9 :
services 

.
 
	AddScoped 
< 
IProductService $
,$ %
ProductService& 4
>4 5
(5 6
)6 7
;7 8
return 
services	 
; 
} 
} ú
†C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CatalogService\BLL\Abstractions\ILinkService.cs
	namespace 	
BLL
 
. 
Abstractions 
; 
public 
	interface 
ILinkService 
{ 
Link 
GenerateLinks 
( 
string 
endpointName (
,( )
object* 0
?0 1
routeValues2 =
,= >
string? E
relF I
,I J
stringK Q
methodR X
)X Y
;Y Z
} 