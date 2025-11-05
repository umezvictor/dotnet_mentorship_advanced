namespace API;
public sealed class ResponseMessage
{
    public const string ItemAddedToCart = "Item added to cart successfully";
    public const string ItemNotAddedToCart = "Item could not be added to cart";
    public const string ItemRemovedFromCart = "Item removed from cart successfully";
    public const string ItemsFetched = "Cart Items fetched successfully";
    public const string ItemNotRemoved = "Item deletion failed";

    public const string NotFound = "No record was found";
    public const string NotItemsPresent = "No items present at the moment";
    public const string Success = "Successful";
}
