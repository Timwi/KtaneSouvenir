using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SOrderPicking
{
    [SouvenirQuestion("What was the order ID in the {1} order of {0}?", ThreeColumns6Answers, ExampleAnswers = ["3141", "7946", "6905", "6408", "5030", "2803", "6918", "6642", "4645", "4356", "2868", "1887"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1000, 9999)]
    Order,

    [SouvenirQuestion("What was the product ID in the {1} order of {0}?", ThreeColumns6Answers, ExampleAnswers = ["3141", "7946", "6905", "6408", "5030", "2803", "6918", "6642", "4645", "4356", "2868", "1887"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1000, 9999)]
    Product,

    [SouvenirQuestion("What was the pallet in the {1} order of {0}?", ThreeColumns6Answers, "CHEP", "SIPPL", "SLPR", "EWHITE", "ECHEP", "ESIPPL", "ESLPR", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Pallet
}

public partial class SouvenirModule
{
    [SouvenirHandler("OrderPickingModule", "Order Picking", typeof(SOrderPicking), "Brawlboxgaming")]
    private IEnumerator<SouvenirInstruction> ProcessOrderPicking(ModuleData module)
    {
        var comp = GetComponent(module, "OrderPickingScript");

        var fldProductId = GetField<int>(comp, "productId");
        var fldOrderId = GetField<int>(comp, "orderNumber");
        var fldPallet = GetField<string>(comp, "pallet");

        var orderCount = GetField<int>(comp, "orderCount").Get();
        var orderList = new int[orderCount];
        var productList = new int[orderCount];
        var palletList = new string[orderCount];

        var fldNewOrder = GetField<int>(comp, "currentOrder");
        var curOrder = 0;

        while (fldNewOrder.Get() <= orderCount)
        {
            var newOrder = fldNewOrder.Get();
            if (curOrder != newOrder)
            {
                curOrder = newOrder;
                orderList[curOrder - 1] = fldOrderId.Get();
                productList[curOrder - 1] = fldProductId.Get();
                palletList[curOrder - 1] = fldPallet.Get();
            }
            yield return new WaitForSeconds(.1f);
        }
        yield return WaitForSolve;

        for (var order = 0; order < orderCount; order++)
        {
            yield return question(SOrderPicking.Order, args: [Ordinal(order + 1)]).Answers(orderList[order].ToString());
            yield return question(SOrderPicking.Product, args: [Ordinal(order + 1)]).Answers(productList[order].ToString());
            yield return question(SOrderPicking.Pallet, args: [Ordinal(order + 1)]).Answers(palletList[order]);
        }
    }
}