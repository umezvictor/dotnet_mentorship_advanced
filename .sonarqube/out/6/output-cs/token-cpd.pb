ç
öC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\BLL\Services\ICartService.cs
	namespace 	
BLL
 
. 
Services 
; 
public 
	interface 
ICartService 
{ 
Task		 
<		 
bool		 

>		
 
AddItemToCartAsync		 
(		   
AddItemToCartRequest		  4
request		5 <
,		< =
CancellationToken		> O
cancellationToken		P a
)		a b
;		b c
Task

 
<

 
bool

 

>


 
DeleteCartItemAsync

 
(

  !%
DeleteItemFromCartRequest

! :
request

; B
,

B C
CancellationToken

D U
cancellationToken

V g
)

g h
;

h i
Task 
< 
Cart 

?
 
> 
GetCartItemsAsync 
(  
string  &
cartKey' .
,. /
CancellationToken0 A
cancellationTokenB S
)S T
;T U
Task 3
'UpdateCartItemsFromMessageConsumerAsync -
(. /"
ProductUpdatedContract/ E
requestF M
,M N
CancellationTokenO `
cancellationTokena r
)r s
;s t
} Û
ôC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\BLL\Services\CartService.cs
	namespace 	
BLL
 
. 
Services 
; 
public 
sealed 
class 
CartService 
(  !
ICartRepository! 0
cartRepository1 ?
)? @
:A B
ICartServiceC O
{		 
public

 
async

 
Task

 
<

 
bool

 
>

 
AddItemToCartAsync

 +
(

, - 
AddItemToCartRequest

- A
request

B I
,

I J
CancellationToken

K \
cancellationToken

] n
)

n o
{ 
return 
await	 
cartRepository 
. 
AddItemAsync *
(* +
request, 3
,3 4
cancellationToken5 F
)G H
;H I
} 
public 
async 
Task 
< 
bool 
> 
DeleteCartItemAsync ,
(- .%
DeleteItemFromCartRequest. G
requestH O
,O P
CancellationTokenQ b
cancellationTokenc t
)t u
{ 
if 
( 
await 
cartRepository 
. 
RemoveItemAsync *
(* +
request, 3
.3 4
CartKey4 ;
,; <
request= D
.D E
IdE G
,G H
cancellationTokenI Z
)[ \
)\ ]
return 	
true
 
; 
return 
false	 
; 
} 
public 
async 
Task 
< 
Cart 
? 
> 
GetCartItemsAsync +
(, -
string- 3
cartKey4 ;
,; <
CancellationToken= N
cancellationTokenO `
)` a
{ 
var 
cart 

= 
await 
cartRepository !
.! "
GetCartItemsAsync" 3
(3 4
cartKey5 <
,< =
cancellationToken> O
)P Q
;Q R
if 
( 
cart 

!= 
null 
) 
return 	
cart
 
; 
return 
null	 
; 
} 
public"" 
async"" 
Task"" 3
'UpdateCartItemsFromMessageConsumerAsync"" :
(""; <"
ProductUpdatedContract""< R
request""S Z
,""Z [
CancellationToken""\ m
cancellationToken""n 
)	"" Ä
{## 
await$$ 
cartRepository$$ 
.$$ 3
'UpdateCartItemsFromMessageConsumerAsync$$ >
($$> ?
request$$@ G
.$$G H
Id$$H J
,$$J K
request$$L S
.$$S T
Price$$T Y
,$$Y Z
request$$[ b
.$$b c
Name$$c g
!$$g h
,$$h i
cancellationToken$$j {
)$$| }
;$$} ~
}%% 
}&& ¢
£C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\BLL\Dtos\DeleteItemFromCartRequest.cs
	namespace 	
BLL
 
. 
Dtos 
; 
public 
sealed 
class %
DeleteItemFromCartRequest -
{ 
[ 
Required 

]
 
public 
int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 

]
 
public		 
required		 
string		 
CartKey		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
}

 î
òC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\CartServices\BLL\DependencyInjection.cs
	namespace 	
CartServices
 
. 
BLL 
; 
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
< 
ICartService !
,! "
CartService# .
>. /
(/ 0
)0 1
;1 2
return 
services	 
; 
} 
} 