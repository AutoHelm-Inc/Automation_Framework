# Simple grammar

Program → {Statement}+ EOF

Statement → ForLoop | SimpleStatement

ForLoop → “for” Int “{“ {Statement}+ “}”

SimpleStatement → Function Arg {“,“ Arg}\*

Function → run | type | saveAs | …

Arg → String | Int

Int→ {Digit}+

Digit → [0-9]
