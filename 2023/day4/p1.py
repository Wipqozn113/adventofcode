class Card:
    def __init__(self, winning, numbers):
        self.winning = winning
        self.numbers = numbers
    
    def Score(self):
        wins = 0
        for win in self.winning:
            wins += 1 if win in self.numbers else 0 
        return 0 if wins == 0 else 2**(wins - 1)

filename = "input.in"
cards = []
total = 0
with open(filename) as file:
    for line in file:
        numbers = line.split(":")[1].split("|")
        winners = numbers[0].strip().split(" ")        
        winners[:] = [i for i in winners if i != '']
        nums = numbers[1].strip().split(" ")
        nums[:] = [i for i in nums if i != '']
        card = Card(winners, nums)
        total += card.Score()
        cards.append(card)

print(total)