

Market test_market = new Market();
market_engine test_engine = new market_engine(1,0,100,100,10, test_market);

for (int i = 0; i < 5 ; i++)
{
	test_engine.execute_one_cycle();
}


