def IsCorrupted(opening, closing):
    j = opening + closing
    return j not in ["()", "[]", "{}", "<>"]

def Score(line):
    stack = []
    points = {"(":1, "[":2, "{":3, "<":4}
    for char in line:
        if char in [")", "]", "}", ">"]:
            if(IsCorrupted(stack.pop(), char)):
                return 0
        else:
            stack.append(char)
    score = 0
    if(len(stack) > 0):
        while len(stack) > 0:
            char = stack.pop()
            score = (score * 5) + points[char]
        return score

    return 0


scores = []
with open('p1.in') as file:
    for line in file:
        score = Score(line.strip())
        if(score > 0):
            scores.append(score)

scores = sorted(scores)

print(scores[round(len(scores)/2)])