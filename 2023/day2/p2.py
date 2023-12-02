import re

class Cubes:
    def __init__(self, red, blue, green):
        self.red = red
        self.green = green
        self.blue = blue

class Game:
    def __init__(self, line):
        ln = line.split(":")
        self.id = int(ln[0].split(" ")[1])
        self.cubes = self.parse_cubes(ln[1])
        self.maxred = 0
        self.maxblue = 0
        self.maxgreen = 0
        self.set_maximums()

    def parse_cubes(self, input):
        cubes = []
        inp = input.split(";")
        for cbs in inp:
            red = re.search('[0-9]+(?= red)', cbs)
            blue = re.search('[0-9]+(?= blue)', cbs)
            green = re.search('[0-9]+(?= green)', cbs)
            cubes.append(Cubes(0 if red is None else int(red.group(0)), 0 if blue is None else int(blue.group(0)), 0 if green is None else int(green.group(0))))
        
        return cubes
    
    def set_maximums(self):
        for cubes in self.cubes:        
            self.maxred = self.maxred if self.maxred > cubes.red else cubes.red
            self.maxblue = self.maxblue if self.maxblue > cubes.blue else cubes.blue
            self.maxgreen = self.maxgreen if self.maxgreen > cubes.green else cubes.green
    
    def is_possible(self, red, green, blue):
        return red >= self.maxred and green >= self.maxgreen and blue >= self.maxblue

    def power(self):
        return self.maxred * self.maxgreen * self.maxblue

def sum_possible_games(games, red, green, blue):
    total = 0
    for game in games:
        total += game.id if game.is_possible(red, green, blue) else 0

    return total

def sum_powers(games):
    total = 0
    for game in games:
        total += game.power()

    return total

games = []
filename = "input.in"
red = 12
blue = 14
green = 13

with open(filename) as file:
    for line in file:
        games.append(Game(line))

print(sum_possible_games(games, red, green, blue))
print(sum_powers(games))




    