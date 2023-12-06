import math

def Solve(time, distance):
    # a = -1 b = time c = -distance
    a = -1
    b = int(time)
    c = -1 * int(distance)
    root1 = (-b + math.sqrt(b**2 - 4*a*c)) / (2 * a)
    root2 = (-b - math.sqrt(b**2 - 4*a*c)) / (2 * a)

    return root1, root2

filename = "test.in"
with open(filename) as file:
    lines = list(file)
time = lines[0].strip().split(":")[1].split()
distance = lines[1].strip().split(":")[1].split()


t = ''.join(time)
d = ''.join(distance)
root1, root2 = Solve(t, d)
wins = (math.ceil(root2)) - (math.floor(root1)) - 1
print(wins)
    