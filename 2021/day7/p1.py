    
import statistics

with open('p1.in') as file:
    for fline in file:
        crabs = [int(x) for x in fline.strip().split(",")]       


med = statistics.median(crabs)
fuel = 0
print(crabs)
for crab in crabs:
    fuel += abs(crab - med)

print(fuel)