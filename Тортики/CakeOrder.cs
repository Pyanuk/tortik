using System;

internal class CakeOrder
{
    private CakeShape cakeShape;
    private CakeSize cakeSize;
    private CakeFlavor cakeFlavor;
    private CakeQuantity cakeQuantity;
    private FrostingType frostingType;
    private DecorType decorType;

    public CakeOrder(CakeShape cakeShape, CakeSize cakeSize, CakeFlavor cakeFlavor, CakeQuantity cakeQuantity, FrostingType frostingType, DecorType decorType)
    {
        this.cakeShape = cakeShape;
        this.cakeSize = cakeSize;
        this.cakeFlavor = cakeFlavor;
        this.cakeQuantity = cakeQuantity;
        this.frostingType = frostingType;
        this.decorType = decorType;
    }

    internal decimal GetTotalCost()
    {
        throw new NotImplementedException();
    }
}