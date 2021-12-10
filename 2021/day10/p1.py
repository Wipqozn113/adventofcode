def IsCorrupted(opening, closing):
    j = opening + closing
    return j not in ["()", "[]", "{}", "<>"]

def Score(line):
    points = {")":3, "]":57, "}":1197, ">":25137}
    first = ""
    stack = []
    for char in line:
        if char in [")", "]", "}", ">"]:
            if(IsCorrupted(stack.pop(), char)):
                return points[char]
        else:
            stack.append(char)

    return 0 

score = 0
with open('p1.in') as file:
    for line in file:
        score += Score(line.strip())

print(score)