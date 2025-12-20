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
		foreach (Order o in orders)
		{
			counter++;
			string info = "";
			info += $"{counter}->ID:{o.id}Type:{o.type} Price:{o.price} Amount:{o.amount} Time:{o.time} ";
			Console.WriteLine(info);
		}
	}
	public void show_orderbook()
	{
		Console.WriteLine($"____ASK'S____[{asks.Count()}]");
		show_orders(asks);
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

	public void match_orders()
	{
		
		sort_orders();

		if (bids.Count > 0 && asks.Count() > 0)
		{
			process();
		}
}
}

