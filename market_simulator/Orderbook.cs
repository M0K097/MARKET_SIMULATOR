public class Orderbook
{
	List<Order> bids = new List<Order>();
	List<Order> asks = new List<Order>();

	int id_counter = 0;
	public void create_order(order_type type, decimal price,double amount,DateTime time)
	{
		id_counter++;
		Order created_order = new Order(id_counter, type, price, amount ,time);
		Console.WriteLine($"new order was created with ID => {id_counter}");
		place_order(created_order);
	}	

	public void place_order(Order order)
	{
		if ( order.type == order_type.bid)
		{
			bids.Add(order);
			Console.WriteLine("Order places");
		}
		else if (order.type == order_type.ask)
		{
			asks.Add(order);
			Console.WriteLine("ask Order places");
		}
	}

	public void show_orders(List<Order> orders)
	{
		int counter = 0;
		int max = 10;
		foreach (Order o in orders)
		{
			if (counter <= max)
			{
				counter++;
				string info = "";
				info += $"{counter}->ID:{o.id} Amount:{o.amount} Price:{o.price}";
				Console.WriteLine(info);
			}
		}
	}
	public void show_orderbook()
	{
		sort_orders();
		var average_price = 0m;
		if(bids.Count() > 0 && asks.Count() > 0)
		{
			var highest_bid = bids.First();
			var lowest_ask = asks.First();
			average_price = (highest_bid.price + lowest_ask.price) / 2;
		}

		Console.Clear();
		Console.WriteLine($"____ASK'S____[{asks.Count()}]");
		asks.Reverse();	
		show_orders(asks);
		Console.WriteLine($"PRICE ------------------------------>"+ average_price);
		Console.WriteLine($"____BIDS'S____[{bids.Count()}]");
		show_orders(bids);
		
	}

	public void sort_orders()
	{
		var sorted_asks = asks.OrderBy(order => order.price)
			.ThenBy(order => order.time).ToList();

		var sorted_bids = bids.OrderByDescending(order => order.price)
			.ThenBy(order => order.time).ToList();
		
		bids = sorted_bids;
		asks = sorted_asks;
	}	

	public void process()
	{
			var highest_bid = bids.First();
			var lowest_ask = asks.First();

			while(highest_bid.price >= lowest_ask.price)
			{
				var capital = (decimal)highest_bid.amount * highest_bid.price;
				if (capital >= lowest_ask.price)
				{
					highest_bid.amount--;
					lowest_ask.amount--;
					capital -= lowest_ask.price;
				}

				if (highest_bid.amount <= 0)
				{
					bids.Remove(highest_bid);
					Console.WriteLine($"ODRED[{highest_bid.id}] filled");
					if (bids.Count() > 0)
					highest_bid = bids.First();
				}

				if(lowest_ask.amount <= 0)
				{
					asks.Remove(lowest_ask);
					Console.WriteLine($"ODRED[{lowest_ask.id}] filled");
					if(asks.Count() > 0)
					lowest_ask = asks.First();
				}

				if(asks.Count() == 0 || bids.Count() == 0)
				{
					break;

				}
			}
	}

	public void execute_market_order(Order market_order)
	{
		Console.WriteLine($"market order was placed: Type:[{market_order.type}] ---> Total_Value: {market_order.price} at Time:{market_order.time}");
		sort_orders();
		var o = market_order;
		var orders_to_match = new List<Order>();
		if (o.type == order_type.bid)
		{
			orders_to_match = bids;
		}
		else if (o.type == order_type.ask)
		{
			orders_to_match = asks;
			orders_to_match.Reverse();
		}

		if(orders_to_match.Count > 0)
		{
			Order best_execution = orders_to_match.First();
			var capital = o.price;
			while(capital >= 0)
			{
				if (capital >= best_execution.price)
				{

					Console.WriteLine($"MarketOrder:{capital}");
					capital -= best_execution.price;
					best_execution.amount--;

				if (best_execution.amount <= 0)
				{
					var old_price = best_execution.price;
					orders_to_match.Remove(best_execution);

				if(orders_to_match.Count() > 0)
					best_execution = orders_to_match.First();
				else
				{
					Console.WriteLine("market order got canceled because market not liquid");
					break;
				}

				Console.WriteLine($"price slipping from:{old_price} to => {best_execution.price}");
				}
				}
				else
				{
					break;
				}
			}
		}
		Console.WriteLine("MARKET ORDER got executed");
	}

	public void match_orders()
	{
		
		sort_orders();
		if (bids.Count() > 0 && asks.Count() > 0)
		{
			process();
		}
	}
}

