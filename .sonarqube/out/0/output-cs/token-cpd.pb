˘
öC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\ResponseObjects\Response.cs
	namespace 	
Shared
 
. 
ResponseObjects  
;  !
public 
class 
Response 
< 
T 
> 
{ 
public 
Response 
( 
) 
{ 
} 
public 
Response 
( 
T 
data 
, 
string  
message! (
)( )
{		 
	Succeeded

 
=

 
true

 
;

 
Message 	
=
 
message 
; 
Data 
= 
data	 
; 
} 
public 
Response 
( 
string 
message  
)  !
{ 
	Succeeded 
= 
true 
; 
Message 	
=
 
message 
; 
} 
public 
Response 
( 
string 
message  
,  !
bool" &
	succeeded' 0
)0 1
{ 
	Succeeded 
= 
	succeeded 
; 
Message 	
=
 
message 
; 
} 
public 
bool 
	Succeeded 
{ 
get 
; 
set !
;! "
}# $
public 
string 
Message 
{ 
get 
; 
set !
;! "
}# $
=% &
string' -
.- .
Empty. 3
;3 4
public 
List 
< 
string 
> 
Errors 
{ 
get !
;! "
set# &
;& '
}( )
=* +
new, /
List0 4
<4 5
string5 ;
>; <
(< =
)= >
;> ?
public 
T 	
?	 

Data 
{ 
get 
; 
set 
; 
} 
} ﬂ

õC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\Dto\UpdateCategoryRequest.cs
	namespace 	
Shared
 
. 
Dto 
; 
public 
class !
UpdateCategoryRequest "
{ 
[ 
System 
. 	
Text	 
. 
Json 
. 
Serialization  
.  !

JsonIgnore! +
]+ ,
public 
int 
Id 
{ 
get 
; 
set 
; 
} 
[		 
Required		 

]		
 
public

 
string

 
Name

 
{

 
get

 
;

 
set

 
;

 
}

  !
public 
string 
Image 
{ 
get 
; 
set 
;  
}! "
=# $
string% +
.+ ,
Empty, 1
;1 2
public 
string 
ParentCategory 
{ 
get  #
;# $
set% (
;( )
}* +
=, -
string. 4
.4 5
Empty5 :
;: ;
} Ê
°C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\ResponseObjects\ResponseMessage.cs
	namespace 	
Shared
 
. 
ResponseObjects  
;  !
public 
sealed 
class 
ResponseMessage #
{ 
public 
const 
string 
ItemAddedToCart $
=% &
$str' H
;H I
public 
const 
string 
ItemNotAddedToCart '
=( )
$str* B
;B C
public 
const 
string 
ItemRemovedFromCart (
=) *
$str+ P
;P Q
public 
const 
string 
ItemsFetched !
=" #
$str$ E
;E F
public 
const 
string 
ItemNotRemoved #
=$ %
$str& <
;< =
public

 
const

 
string

 
CategoryAdded

 "
=

# $
$str

% F
;

F G
public 
const 
string 
CategoryUpdated $
=% &
$str' J
;J K
public 
const 
string 
CategoryDeleted $
=% &
$str' J
;J K
public 
const 
string 
CategoryNotFound %
=& '
$str( <
;< =
public 
const 
string 
ProductAdded !
=" #
$str$ D
;D E
public 
const 
string 
ProductUpdated #
=$ %
$str& H
;H I
public 
const 
string 
ProductDeleted #
=$ %
$str& H
;H I
public 
const 
string 
ProductNotFound $
=% &
$str' :
;: ;
public 
const 
string 
NotFound 
= 
$str  5
;5 6
public 
const 
string 
NotItemsPresent $
=% &
$str' G
;G H
public 
const 
string 
Success 
= 
$str +
;+ ,
public 
const 
string 
Failure 
= 
$str '
;' (
} ∏
°C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\RabbitMQ\ProductUpdatedContract.cs
	namespace 	
Shared
 
. 
RabbitMQ 
; 
public 
class "
ProductUpdatedContract #
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
string 
? 
Name 
{ 
get 
; 
set 
;  
}! "
public 
decimal 
Price 
{ 
get 
; 
set  
;  !
}" #
} ∞
£C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\ResponseObjects\PaginatedResponse.cs
	namespace 	
Shared
 
. 
ResponseObjects  
;  !
public 
class 
PaginatedResponse 
< 
T  
>  !
where" '
T( )
:* +
class, 1
{ 
public 
T 	
?	 

Data 
{ 
get 
; 
set 
; 
} 
public 
long 

TotalCount 
{ 
get 
; 
set "
;" #
}$ %
public 
int 
PageSize 
{ 
get 
; 
set 
;  
}! "
} ‹
úC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\RabbitMQ\RabbitMQConstants.cs
	namespace 	
Shared
 
. 
RabbitMQ 
; 
public 
class 
RabbitMQConstants 
{ 
public 
const 
string 
ProductQueue !
=" #
$str$ 3
;3 4
public 
const 
string 
Host 
= 
$str '
;' (
} Ù
öC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\Dto\UpdateProductRequest.cs
	namespace 	
Shared
 
. 
Dto 
; 
public 
class  
UpdateProductRequest !
{ 
[ 
System 
. 	
Text	 
. 
Json 
. 
Serialization  
.  !

JsonIgnore! +
]+ ,
public 
long 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 

]
 
public		 
string		 
Name		 
{		 
get		 
;		 
set		 
;		 
}		  !
public

 
string

 
Image

 
{

 
get

 
;

 
set

 
;

  
}

! "
=

# $
string

% +
.

+ ,
Empty

, 1
;

1 2
public 
string 
Description 
{ 
get  
;  !
set" %
;% &
}' (
=) *
string+ 1
.1 2
Empty2 7
;7 8
[ 
Required 

]
 
public 
decimal 
Price 
{ 
get 
; 
set  
;  !
}" #
[ 
Required 

]
 
public 
int 
Amount 
{ 
get 
; 
set 
; 
}  
} ¯
êC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\Dto\ProductDto.cs
	namespace 	
Shared
 
. 
Dto 
; 
public 
class 

ProductDto 
{ 
public 
long 
Id 
{ 
get 
; 
set 
; 
} 
public 
string 
Name 
{ 
get 
; 
set 
; 
}  !
=" #
string$ *
.* +
Empty+ 0
;0 1
public 
string 
Description 
{ 
get  
;  !
set" %
;% &
}' (
=) *
string+ 1
.1 2
Empty2 7
;7 8
public 
decimal 
Price 
{ 
get 
; 
set  
;  !
}" #
public		 
int		 
Amount		 
{		 
get		 
;		 
set		 
;		 
}		  
public

 
int

 

CategoryId

 
{

 
get

 
;

 
set

 !
;

! "
}

# $
public 
string 
Image 
{ 
get 
; 
set 
;  
}! "
=# $
string% +
.+ ,
Empty, 1
;1 2
public 
List 
< 
Link 
> 
Links 
{ 
get 
; 
set  #
;# $
}% &
=' (
new) ,
(, -
)- .
;. /
} 
public 
record 
Link 
( 
string 
Href 
,  
string! '
Rel( +
,+ ,
string- 3
Method4 :
): ;
;; <—
ñC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\Dto\GetProductsQuery.cs
	namespace 	
Shared
 
. 
Dto 
; 
public 
class 
GetProductsQuery 
{ 
[ 
Required 

]
 
public 
int 

CategoryId 
{ 
get 
; 
set !
;! "
}# $
public 
int 

PageNumber 
{ 
get 
; 
set !
;! "
}# $
public		 
int		 
PageSize		 
{		 
get		 
;		 
set		 
;		  
}		! "
} ù
öC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\Dto\DeleteProductRequest.cs
	namespace 	
Shared
 
. 
Dto 
; 
public 
class  
DeleteProductRequest !
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
} ü
õC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\Dto\DeleteCategoryRequest.cs
	namespace 	
Shared
 
. 
Dto 
; 
public 
class !
DeleteCategoryRequest "
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
} ∫
ëC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\Dto\CategoryDto.cs
	namespace 	
Shared
 
. 
Dto 
; 
public 
class 
CategoryDto 
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
}  !
=" #
string$ *
.* +
Empty+ 0
;0 1
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
public 
string 
ParentCategory 
{ 
get  #
;# $
set% (
;( )
}* +
=, -
string. 4
.4 5
Empty5 :
;: ;
public 
List 
< 
Link 
> 
Links 
{ 
get 
; 
set  #
;# $
}% &
=' (
new) ,
(, -
)- .
;. /
}		 »
óC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\Dto\AddProductRequest.cs
	namespace 	
Shared
 
. 
Dto 
; 
public 
class 
AddProductRequest 
{ 
[ 
Required 

]
 
public 
string 
Name 
{ 
get 
; 
set 
; 
}  !
public 
string 
Image 
{ 
get 
; 
set 
;  
}! "
=# $
string% +
.+ ,
Empty, 1
;1 2
public		 
string		 
Description		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
=		) *
string		+ 1
.		1 2
Empty		2 7
;		7 8
public

 
decimal

 
Price

 
{

 
get

 
;

 
set

  
;

  !
}

" #
public 
int 
Amount 
{ 
get 
; 
set 
; 
}  
public 
int 

CategoryId 
{ 
get 
; 
set !
;! "
}# $
} Å
òC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\Dto\AddCategoryRequest.cs
	namespace 	
Shared
 
. 
Dto 
; 
public 
class 
AddCategoryRequest 
{ 
[ 
Required 

]
 
public 
string 
Name 
{ 
get 
; 
set 
; 
}  !
public 
string 
Image 
{ 
get 
; 
set 
;  
}! "
=# $
string% +
.+ ,
Empty, 1
;1 2
public		 
string		 
ParentCategory		 
{		 
get		  #
;		# $
set		% (
;		( )
}		* +
=		, -
string		. 4
.		4 5
Empty		5 :
;		: ;
} ‡
òC:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\Shared\Constants\AppConstants.cs
	namespace 	
Shared
 
. 
	Constants 
; 
public 
class 
AppConstants 
{ 
public 
const 
string 

CorsPolicy 
=  !
$str" .
;. /
public 
const 
string 
RateLimitingPolicy '
=( )
$str* >
;> ?
} 