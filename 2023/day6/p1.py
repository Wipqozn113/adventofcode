import math

def Solve(time, distance):
    # a = -1 b = time c = -distance
    a = -1
    b = int(time)
    c = -1 * int(distance)
    root1 = (-b + math.sqrt(b**2 - 4*a*c)) / (2 * a)
    root2 = (-b - math.sqrt(b**2 - 4*a*c)) / (2 * a)

    return root1, root2

filename = "input.in"
with open(filename) as file:
    lines = list(file)
time = lines[0].strip().split(":")[1].split()
distance = lines[1].strip().split(":")[1].split()

wins = 1
while len(time) > 0:
    t = time.pop()
    d = distance.pop()
    root1, root2 = Solve(t, d)
    wins *= (math.ceil(root2)) - (math.floor(root1)) - 1
    #print(w)
print(wins)
    