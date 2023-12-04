class Card:
    def __init__(self, winning, numbers):
        self.winning = winning
        self.numbers = numbers
        self.copies = 1
    
    def Score(self):
        wins = 0
        for win in self.winning:
            wins += 1 if win in self.numbers else 0 
        return 0 if wins == 0 else 2**(wins - 1)
    
    def WinCount(self):
        wins = 0
        for win in self.winning:
            wins += 1 if win in self.numbers else 0 
        return wins

filename = "input.in"
cards = []
total = 0
with open(filename) as file:
    for line in file:
        numbers = line.split(":")[1].split("|")
        winners = numbers[0].strip().split()        
        nums = numbers[1].strip().split()
        card = Card(winners, nums)
        total += card.Score()
        cards.append(card)

print(total)
total = 0
i = 0 
for card in cards:
    total += card.copies
    wins = card.WinCount()
    for n in range(i + 1, i + wins + 1):
        cards[n].copies += card.copies
    i += 1
print(total)


