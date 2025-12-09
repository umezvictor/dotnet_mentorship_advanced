ì	
ñC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\DAL\Entities\CartItem.cs
	namespace 	
DAL
 
. 
Entities 
; 
public 
class 
CartItem 
{ 
public 
int 
Id 
{ 
get 
; 
set 
; 
} 
public 
string 
Name 
{ 
get 
; 
set 
; 
}  !
public 
string 
Image 
{ 
get 
; 
set 
;  
}! "
=# $
string% +
.+ ,
Empty, 1
;1 2
public 
decimal 
Price 
{ 
get 
; 
set  
;  !
}" #
public 
int 
Quantity 
{ 
get 
; 
set 
;  
}! "
}		 ò
íC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\DAL\Entities\Cart.cs
	namespace 	
DAL
 
. 
Entities 
; 
public 
class 
Cart 
{ 
[ 
BsonId 
] 	
public 
string 
CartKey 
{ 
get 
; 
set !
;! "
}# $
public		 
List		 
<		 
CartItem		 
>		 
	CartItems		  
{		! "
get		# &
;		& '
set		( +
;		+ ,
}		- .
=		/ 0
[		1 2
]		2 3
;		3 4
} Ñ
òC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\DAL\DependencyInjection.cs
	namespace 	
CartServices
 
. 
DAL 
; 
public 
static 
class 
DependencyInjection '
{		 
public

 
static

 
IServiceCollection

 !
AddDataAccessLayer

" 4
(

5 6
this

6 :
IServiceCollection

; M
services

N V
,

V W
IConfiguration

X f
configuration

g t
)

t u
{ 
return 
services	 
. 
AddMongoDbClient 
( 
configuration #
)$ %
. 
AddCartRepository 
( 
) 
; 
} 
private 
static	 
IServiceCollection "
AddMongoDbClient# 3
(4 5
this5 9
IServiceCollection: L
servicesM U
,U V
IConfigurationW e
configurationf s
)s t
{ 
var 
dbConnectionString 
= 
configuration (
[( )
$str) D
]D E
;E F
var 
mongoUrl 
= 
new 
MongoUrl 
( 
dbConnectionString 1
)2 3
;3 4
var 
mongoClient 
= 
new 
MongoClient #
(# $
mongoUrl% -
). /
;/ 0
services 

.
 
AddSingleton 
< 
IMongoClient $
>$ %
(% &
mongoClient' 2
)3 4
;4 5
services 

.
 
	AddScoped 
< 
IMongoDatabase #
># $
($ %
sp& (
=>) +
{ 
var 
client 
= 
sp 
. 
GetRequiredService %
<% &
IMongoClient& 2
>2 3
(3 4
)4 5
;5 6
return 	
client
 
. 
GetDatabase 
( 
mongoUrl &
.& '
DatabaseName' 3
)4 5
;5 6
} 
) 
; 
return!! 
services!!	 
;!! 
}"" 
private$$ 
static$$	 
IServiceCollection$$ "
AddCartRepository$$# 4
($$5 6
this$$6 :
IServiceCollection$$; M
services$$N V
)$$V W
{%% 
services&& 

.&&
 
	AddScoped&& 
<&& 
ICartRepository&& $
,&&$ %
CartRepository&&& 4
>&&4 5
(&&5 6
)&&6 7
;&&7 8
return'' 
services''	 
;'' 
}(( 
})) «
®C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\DAL\Database\Repository\ICartRepository.cs
	namespace 	
CartServices
 
. 
DAL 
. 
Database #
.# $

Repository$ .
;. /
public 
	interface 
ICartRepository  
{ 
Task		 
<		 
bool		 

>		
 
AddItemAsync		 
(		  
AddItemToCartRequest		 .
cart		/ 3
,		3 4
CancellationToken		5 F
cancellationToken		G X
)		X Y
;		Y Z
Task

 
<

 
Cart

 

>


 
GetCartItemsAsync

 
(

 
string

 %
cartKey

& -
,

- .
CancellationToken

/ @
cancellationToken

A R
)

R S
;

S T
Task 
< 
bool 

>
 
RemoveItemAsync 
( 
string #
cartKey$ +
,+ ,
int- 0
itemId1 7
,7 8
CancellationToken9 J
cancellationTokenK \
)\ ]
;] ^
Task 3
'UpdateCartItemsFromMessageConsumerAsync -
(. /
int/ 2
id3 5
,5 6
decimal7 >
price? D
,D E
stringF L
nameM Q
,Q R
CancellationTokenS d
cancellationTokene v
)v w
;w x
} çR
ßC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\DAL\Database\Repository\CartRepository.cs
	namespace 	
CartServices
 
. 
DAL 
. 
Database #
.# $

Repository$ .
;. /
public 
sealed 
class 
CartRepository "
:# $
ICartRepository% 4
{ 
private 
const	 
string 
CollectionName $
=% &
$str' -
;- .
private		 
readonly			 
IMongoCollection		 "
<		" #
Cart		# '
>		' (
_collection		) 4
;		4 5
public 
CartRepository 
( 
IMongoDatabase &
database' /
)/ 0
{ 
_collection 
= 
database 
. 
GetCollection &
<& '
Cart' +
>+ ,
(, -
CollectionName. <
)= >
;> ?
} 
public 
async 
Task 
< 
bool 
> 
AddItemAsync %
(& ' 
AddItemToCartRequest' ;
cart< @
,@ A
CancellationTokenB S
cancellationTokenT e
)e f
{ 
var 
existingCart 
= 
await 
_collection &
.& '
Find' +
(+ ,
c- .
=>/ 1
c2 3
.3 4
CartKey4 ;
==< >
cart? C
.C D
CartKeyD K
)L M
.M N
FirstOrDefaultAsyncN a
(a b
cancellationTokenc t
)u v
;v w
if 
( 
existingCart 
!= 
null 
) 
{ 
existingCart 
. 
	CartItems 
. 
Add 
( 
cart #
.# $
CartItem$ ,
)- .
;. /
var 
update 
= 
Builders 
< 
Cart 
> 
. 
Update %
.% &
Set& )
() *
c+ ,
=>- /
c0 1
.1 2
	CartItems2 ;
,; <
existingCart= I
.I J
	CartItemsJ S
)T U
;U V
var 
result 
= 
await 
_collection !
.! "
UpdateOneAsync" 0
(0 1
c2 3
=>4 6
c7 8
.8 9
CartKey9 @
==A C
cartD H
.H I
CartKeyI P
,P Q
updateR X
,X Y
cancellationTokenZ k
:k l
cancellationTokenm ~
)	 Ä
;
Ä Å
return 	
result
 
. 
ModifiedCount 
>  
$num! "
;" #
} 
else 
{ 
var 
newCart 
= 
new 
Cart 
{ 
CartKey 
= 
cart 
. 
CartKey 
, 
	CartItems 
= 
new 
List 
< 
CartItem !
>! "
{# $
cart% )
.) *
CartItem* 2
}3 4
}   
;   
await!! 
_collection!!	 
.!! 
InsertOneAsync!! #
(!!# $
newCart!!% ,
,!!, -
options!!. 5
:!!5 6
null!!7 ;
,!!; <
cancellationToken!!= N
)!!O P
;!!P Q
return"" 	
true""
 
;"" 
}## 
}$$ 
public&& 
async&& 
Task&& 
<&& 
Cart&& 
>&& 
GetCartItemsAsync&& *
(&&+ ,
string&&, 2
cartKey&&3 :
,&&: ;
CancellationToken&&< M
cancellationToken&&N _
)&&_ `
{'' 
return(( 
await((	 
_collection(( 
.(( 
Find(( 
(((  
c((! "
=>((# %
c((& '
.((' (
CartKey((( /
==((0 2
cartKey((3 :
)((; <
.((< =
FirstOrDefaultAsync((= P
(((P Q
cancellationToken((R c
)((d e
;((e f
})) 
public,, 
async,, 
Task,, 
<,, 
bool,, 
>,, 
RemoveItemAsync,, (
(,,) *
string,,* 0
cartKey,,1 8
,,,8 9
int,,: =
itemId,,> D
,,,D E
CancellationToken,,F W
cancellationToken,,X i
),,i j
{-- 
var.. 
cart.. 

=.. 
await.. 
_collection.. 
... 
Find.. #
(..# $
c..% &
=>..' )
c..* +
...+ ,
CartKey.., 3
==..4 6
cartKey..7 >
)..? @
...@ A
FirstOrDefaultAsync..A T
(..T U
cancellationToken..V g
)..h i
;..i j
if// 
(// 
cart// 

is// 
null// 
)// 
return00 	
false00
 
;00 
var33 
itemToRemove33 
=33 
cart33 
.33 
	CartItems33 #
.33# $
FirstOrDefault33$ 2
(332 3
i334 5
=>336 8
i339 :
.33: ;
Id33; =
==33> @
itemId33A G
)33H I
;33I J
if44 
(44 
itemToRemove44 
is44 
null44 
)44 
return55 	
false55
 
;55 
cart77 
.77 
	CartItems77 
.77 
Remove77 
(77 
itemToRemove77 %
)77& '
;77' (
var99 
update99 
=99 
Builders99 
<99 
Cart99 
>99 
.99 
Update99 $
.99$ %
Set99% (
(99( )
c99* +
=>99, .
c99/ 0
.990 1
	CartItems991 :
,99: ;
cart99< @
.99@ A
	CartItems99A J
)99K L
;99L M
var:: 
result:: 
=:: 
await:: 
_collection::  
.::  !
UpdateOneAsync::! /
(::/ 0
c::1 2
=>::3 5
c::6 7
.::7 8
CartKey::8 ?
==::@ B
cartKey::C J
,::J K
update::L R
,::R S
cancellationToken::T e
:::e f
cancellationToken::g x
)::y z
;::z {
return;; 
result;;	 
.;; 
ModifiedCount;; 
>;; 
$num;;  !
;;;! "
}>> 
publicBB 
asyncBB 
TaskBB 3
'UpdateCartItemsFromMessageConsumerAsyncBB :
(BB; <
intBB< ?
idBB@ B
,BBB C
decimalBBD K
priceBBL Q
,BBQ R
stringBBS Y
nameBBZ ^
,BB^ _
CancellationTokenBB` q
cancellationToken	BBr É
)
BBÉ Ñ
{CC 
varEE 
cartsWithItemEE 
=EE 
awaitEE 
_collectionEE '
.EE' (
FindEE( ,
(EE, -
BuildersEE. 6
<EE6 7
CartEE7 ;
>EE; <
.EE< =
FilterEE= C
.EEC D
	ElemMatchEED M
(EEM N
cEEO P
=>EEQ S
cEET U
.EEU V
	CartItemsEEV _
,EE_ `
ciEEa c
=>EEd f
ciEEg i
.EEi j
IdEEj l
==EEm o
idEEp r
)EEs t
)EEu v
.EEv w
ToListAsync	EEw Ç
(
EEÇ É
cancellationToken
EEÑ ï
)
EEñ ó
;
EEó ò
ifFF 
(FF 
!FF 
cartsWithItemFF 
.FF 
AnyFF 
(FF 
)FF 
)FF 
returnGG 	
;GG	 

varHH 
updatedCountHH 
=HH 
$numHH 
;HH 
foreachII 	
(II
 
varII 
itemII 
inII 
cartsWithItemII $
)II$ %
{JJ 
updatedCountKK 
++KK 
;KK 
varLL 
	cartItemsLL 
=LL 
itemLL 
.LL 
	CartItemsLL !
;LL! "
foreachMM 

(MM 
varMM 
cartItemMM 
inMM 
	cartItemsMM %
.MM% &
WhereMM& +
(MM+ ,
ciMM- /
=>MM0 2
ciMM3 5
.MM5 6
IdMM6 8
==MM9 ;
idMM< >
)MM? @
)MM@ A
{NN 
cartItemOO 
.OO 
NameOO 
=OO 
nameOO 
;OO 
cartItemPP 
.PP 
PricePP 
=PP 
pricePP 
;PP 
}QQ 
varRR 
updateRR 
=RR 
BuildersRR 
<RR 
CartRR 
>RR 
.RR 
UpdateRR %
.RR% &
SetRR& )
(RR) *
cRR+ ,
=>RR- /
cRR0 1
.RR1 2
	CartItemsRR2 ;
,RR; <
	cartItemsRR= F
)RRG H
;RRH I
varSS 
resultSS 
=SS 
awaitSS 
_collectionSS !
.SS! "
UpdateOneAsyncSS" 0
(SS0 1
cSS2 3
=>SS4 6
cSS7 8
.SS8 9
CartKeySS9 @
==SSA C
itemSSD H
.SSH I
CartKeySSI P
,SSP Q
updateSSR X
,SSX Y
cancellationTokenSSZ k
:SSk l
cancellationTokenSSm ~
)	SS Ä
;
SSÄ Å
ifTT 
(TT 
resultTT 
.TT 
ModifiedCountTT 
>TT 
$numTT 
)TT  
updatedCountUU 
++UU 
;UU 
}WW 
}XX 
}YY œ
ôC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\DAL\AddItemToCartRequest.cs
	namespace 	
DAL
 
; 
public 
class  
AddItemToCartRequest !
{ 
[ 
Required 

]
 
public 
string 
CartKey 
{ 
get 
; 
set !
;! "
}# $
[		 
Required		 

]		
 
public

 
CartItem

 
CartItem

 
{

 
get

 
;

  
set

! $
;

$ %
}

& '
} 
public 
class 
UpdateItemRequest 
{ 
public 
CartItem 
CartItem 
{ 
get 
;  
set! $
;$ %
}& '
} 