
"""
A = P(1 + R/100) t
Compound Interest = A – P
Where,
A is amount
P is principle amount
R is the rate and
T is the time span
"""

def compound_interest(principle, rate, time):

    Amount = principle * (pow((1 + rate / 100), time))
    CI = Amount - principle
    print("Compound interest is", CI)

compound_interest(5000000, 5.5, 10)