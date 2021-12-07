import statistics


with open('p1.in') as file:
    for fline in file:
        crabs = [int(x) for x in fline.strip().split(",")]       


start = min(crabs)
stop = max(crabs)
minfuel = None
for pos in range(start, stop + 1):
    fuel = 0
    for crab in crabs:
        n =  abs(crab - pos)
        fuel += ((n * (n + 1)) / 2)

    if minfuel is None or minfuel > fuel:
        minfuel = fuel

print(minfuel)