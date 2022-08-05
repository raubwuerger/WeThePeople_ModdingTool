def canTrigger$NATION_NORMAL$TradeQuest_$YIELD$_START(argsList):
	
	# Read Parameters 1+2 from the two events and check if enough yield is stored in city
	eEvent = gc.getInfoTypeForString("EVENT_$NATION_UPPERCASE$_TRADE_QUEST_$YIELD$_START")
	event = gc.getEventInfo(eEvent)
	iYieldID = event.getGenericParameter(2)
	iQuantity = event.getGenericParameter(1) # for Quest Start this should be e.g. 200

	# Now we call the Generic Helper Function
	bTrigger = CanDo$NATION_NORMAL$Trade(argsList, iYieldID, iQuantity)
	
	return bTrigger
