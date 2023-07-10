# Complex grammar for future implementation

Program → {Statement}+ EOF

Statement → ForLoop | IfElse | SimpleStatement

ForLoop → “for” Int “{“ {Statement}+ “}”

IfElse → “if” Expr “{“ {Statement}+ “}” “else {“ {Statement}+ ”}”

SimpleStatement → VarDefinition | VarAssignment | Expr

VarDefinition → “var” Var “=” Expr

VarAssignment → Var “=” Expr

Expr → FunctionCall | String | Int

FunctionCall → Function Expr {“,“ Expr}\*

Var → String

Int→ {Digit}+

Digit → [0-9]
