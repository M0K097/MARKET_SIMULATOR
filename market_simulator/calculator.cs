public class market_engine
{	
	Random dice = new Random();
	int limit_orders_per_cycle {get; set;}
	int market_orders_per_cycle {get; set;}
	Market market {get;set;}
	int max_price_generated {get;set;}
	int max_amount_generated {get;set;}
	int market_regime {get;set;} // 100 = balanced ; < 100 = bear ; > 100 = bull

	public void start_engine()
	{
		


	}

	public void execute_one_cycle()
	{

		for(int i = 0; i < limit_orders_per_cycle ; i++)
		{
			var type = dice.Next(market_regime);
			var price = dice.Next(max_price_generated);
			var amount = dice.Next(max_amount_generated);
			Console.WriteLine($"random order data = type:{type} price:{price}, amount:{amount}");
			if(type < 50)
			{
				market.create_limit_order_to_sell(price,amount);
			}
			else
			{
				market.create_limit_order_to_buy(price,amount);
			}

		}
		for(int m = 0; m < market_orders_per_cycle ; m++)
		{
			var type = dice.Next(market_regime);
			var price = dice.Next(max_price_generated);
			var amount = dice.Next(max_amount_generated);
			Console.WriteLine("creating market order...");
			Console.WriteLine($"random order data = type:{type} price:{price}, amount:{amount}");

			var total_price = price * amount;
			if(type < 50)
			{
				market.create_market_order(order_type.ask,total_price);
			}
			else
			{
				market.create_market_order(order_type.bid,total_price);

			}


		}
			market.complete_one_cycle();

	}




	public market_engine(int limit_orders_per_cycle, int market_os_per_cycle,int market_regime, int max_price, int max_amount,Market market)
	{
		this.market = market;
		this.market_regime = market_regime;
		this.limit_orders_per_cycle = limit_orders_per_cycle;
		this.market_orders_per_cycle = market_os_per_cycle;
		max_price_generated = max_price;
		max_amount_generated = max_amount;
		
	}
	
}
