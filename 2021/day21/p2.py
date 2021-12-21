rolls = {3: 1, 4: 3, 5: 6, 6: 7, 7: 6, 8: 3, 9: 1}
p1 = {}
p2 = {}
games = {}

def CountGames(games):
    count = 0 
    for game in games:
        count += games[game]

    print(count)

def GetKey(p1val, p1pos, p2val, p2pos):
    return f"{p1val}|{p1pos}-{p2val}|{p2pos}"

def GetValues(key):
    p1, p2 = [x for x in key.split("-")]
    p1val, p1pos = [int(x) for x in p1.split("|")]
    p2val, p2pos = [int(x) for x in p2.split("|")]

    return p1val, p1pos, p2val, p2pos

def GetPositionScore(score, pos, roll):
    pos = (pos + roll) % 10
    pos = 10 if pos == 0 else pos
    score += pos

    return pos, score

def Roll(rolls, p1start, p2start):
    # First iteration
    oldgames = {}
    oldgames[GetKey(0, p1start, 0, p2start)] = 0
    
    p1wins = 0
    p2wins = 0
    while True:
        # player 1 turn
        games = {}
        for game in oldgames:
            p1val, p1pos, p2val, p2pos = GetValues(game)
            for roll in rolls:
                pos, val = GetPositionScore(p1val, p1pos, roll)
                count = oldgames[game] * rolls[roll]  if oldgames[game] != 0 else rolls[roll]

                if val >= 21:
                    p1wins += count                
                else:
                    key = GetKey(val, pos, p2val, p2pos)
                    games[key] = count if key not in games else count + games[key]

        if(len(games) == 0):
            break

        oldgames = games

        # player 2 turn
        games = {}
        for game in oldgames:
            p1val, p1pos, p2val, p2pos = GetValues(game)
            for roll in rolls:
                pos, val = GetPositionScore(p2val, p2pos, roll)
 
                count = oldgames[game] * rolls[roll]  
                if val >= 21:
                    p2wins += count                
                else:
                    key = GetKey(p1val, p1pos, val, pos)
                    games[key] = count if key not in games else count + games[key]

        if(len(games) == 0):
            break
        oldgames = games      

    print(p1wins, p2wins)   

p1 = None
with open('p1.in') as file:
    for line in file:
        line = line.split(":")
        if p1 == None:
            p1 = line[1].strip()
        else:
            p2 = line[1].strip()

Roll(rolls, p1, p2)
    


        