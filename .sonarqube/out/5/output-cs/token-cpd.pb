Ø
“C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\RabbitMQ\IRabbitMqClient.cs
	namespace 	
RabbitMQ
 
; 
public 
	interface 
IRabbitMqClient  
{ 
Task 
< 
string 
> 
ConsumeMessageAsync !
(" #
string# )
	queueName* 3
)3 4
;4 5
Task 
PublishMessageAsync 
< 
T 
> 
( 
T 
message  '
,' (
string) /
	queueName0 9
)9 :
;: ;
} ¸5
’C:\Users\ChibuzorUmezuruike\Documents\Victor\.Net_Mentorship_Program_Advanced\DotnetAdvancedEcommerceProject\src\Shared\RabbitMQ\RabbitMqClient.cs
	namespace 	
RabbitMQ
 
; 
public 
sealed 
class 
RabbitMqClient "
:# $
IRabbitMqClient% 4
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
PublishMessageAsync

 &
<

& '
T

' (
>

( )
(

* +
T

+ ,
message

- 4
,

4 5
string

6 <
	queueName

= F
)

F G
{ 
var 
factory 
= 
new 
ConnectionFactory %
{& '
HostName( 0
=1 2
RabbitMQConstants3 D
.D E
HostE I
}J K
;K L
factory 	
.	 
$
AutomaticRecoveryEnabled
 "
=# $
true% )
;) *
factory 	
.	 
#
NetworkRecoveryInterval
 !
=" #
TimeSpan$ ,
., -
FromSeconds- 8
(8 9
$num: <
)= >
;> ?
using 
var 

connection 
= 
await 
factory &
.& '!
CreateConnectionAsync' <
(< =
)= >
;> ?
using 
var 
channel 
= 
await 

connection &
.& '
CreateChannelAsync' 9
(9 :
): ;
;; <
await 
channel 
. 
QueueDeclareAsync !
(! "
queue# (
:( )
	queueName* 3
,3 4
durable5 <
:< =
false> C
,C D
	exclusiveE N
:N O
falseP U
,U V

autoDeleteW a
:a b
falsec h
,h i
	arguments 
: 
null 
) 
; 
var 
json 

= 
JsonSerializer 
. 
	Serialize %
(% &
message' .
)/ 0
;0 1
var 
body 

= 
Encoding 
. 
UTF8 
. 
GetBytes #
(# $
json% )
)* +
;+ ,
await 
channel 
. 
BasicPublishAsync !
(! "
exchange# +
:+ ,
string- 3
.3 4
Empty4 9
,9 :

routingKey; E
:E F
	queueNameG P
,P Q
bodyR V
:V W
bodyX \
)] ^
;^ _
} 
public 
async 
Task 
< 
string 
> 
ConsumeMessageAsync .
(/ 0
string0 6
	queueName7 @
)@ A
{ 
var 
factory 
= 
new 
ConnectionFactory %
{& '
HostName( 0
=1 2
RabbitMQConstants3 D
.D E
HostE I
}J K
;K L
factory 	
.	 
$
AutomaticRecoveryEnabled
 "
=# $
true% )
;) *
factory 	
.	 
#
NetworkRecoveryInterval
 !
=" #
TimeSpan$ ,
., -
FromSeconds- 8
(8 9
$num: <
)= >
;> ?
using   
var   

connection   
=   
await   
factory   &
.  & '!
CreateConnectionAsync  ' <
(  < =
)  = >
;  > ?
using!! 
var!! 
channel!! 
=!! 
await!! 

connection!! &
.!!& '
CreateChannelAsync!!' 9
(!!9 :
)!!: ;
;!!; <
await## 
channel## 
.## 
QueueDeclareAsync## !
(##! "
queue### (
:##( )
RabbitMQConstants##* ;
.##; <
ProductQueue##< H
,##H I
durable##J Q
:##Q R
false##S X
,##X Y
	exclusive##Z c
:##c d
false##e j
,##j k

autoDelete##l v
:##v w
false##x }
,##} ~
	arguments$$ 
:$$ 
null$$ 
)$$ 
;$$ 
var&& 
response&& 
=&& 
string&& 
.&& 
Empty&& 
;&& 
var(( 
consumer(( 
=(( 
new(( &
AsyncEventingBasicConsumer(( /
(((/ 0
channel((1 8
)((9 :
;((: ;
consumer)) 

.))
 
ReceivedAsync)) 
+=)) 
async)) !
())" #
model))# (
,))( )
arg))* -
)))- .
=>))/ 1
{** 
var++ 
body++ 
=++ 
arg++ 
.++ 
Body++ 
.++ 
ToArray++ 
(++ 
)++  
;++  !
var,, 
message,, 
=,, 
Encoding,, 
.,, 
UTF8,, 
.,, 
	GetString,, (
(,,( )
body,,* .
),,/ 0
;,,0 1
try.. 
{// 
response00 
=00 
message00 
;00 
await11 	
channel11
 
.11 
BasicAckAsync11 
(11  
deliveryTag11! ,
:11, -
arg11. 1
.111 2
DeliveryTag112 =
,11= >
multiple11? G
:11G H
false11I N
)11O P
;11P Q
}22 
catch33 
{44 
await55 	
channel55
 
.55 
BasicNackAsync55  
(55  !
deliveryTag55" -
:55- .
arg55/ 2
.552 3
DeliveryTag553 >
,55> ?
multiple55@ H
:55H I
false55J O
,55O P
requeue55Q X
:55X Y
true55Z ^
)55_ `
;55` a
}66 
return77 	
;77	 

}88 
;88 
await99 
channel99 
.99 
BasicConsumeAsync99 !
(99! "
RabbitMQConstants99# 4
.994 5
ProductQueue995 A
,99A B
autoAck99C J
:99J K
false99L Q
,99Q R
consumer99S [
:99[ \
consumer99] e
)99f g
;99g h
return;; 
response;;	 
;;; 
}<< 
}== 