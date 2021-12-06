class LanternFish:
    def __init__(self, days):
        self.timer = days

    def SimulateDay(self):
        fish = None
        self.timer -= 1
        if(self.timer == -1):
            self.timer = 6
            fish = LanternFish(8)

        return fish

class Swarm:
    def __init__(self, fish):
        self.fish = []
        fisharr = fish.strip().split(",")
        for f in fisharr:
            days = int(f.strip())
            self.fish.append(LanternFish(days))

    def SimulateNDays(self, days):
        for d in range(days):
            print("On day " + str(d) + "...")
            self.SimulateDay()

        return len(self.fish)

    def SimulateDay(self):
        newfish = []
        for fish in self.fish:
            f = fish.SimulateDay()
            if f is not None:
                newfish.append(f)
        
        self.fish += newfish

    
with open('p1.in') as file:
    for fline in file:
        fish = fline

swarm = Swarm(fish)
print(swarm.SimulateNDays(80))
