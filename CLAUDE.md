# Instructions for AI Agents

**Before working on this project, read `.claude/README.md` for quick reference, templates, and coding conventions.**

This file provides essential project context for creating new features and components following the Nebula project's established patterns.

## Working Guidelines

### Ask Clarifying Questions
Before starting work on a task, ask clarifying questions if requirements are ambiguous or incomplete. It's better to confirm intent upfront than to make incorrect assumptions.

### Break Down Large Tasks
Decompose large or complex tasks into smaller, manageable subtasks. Use the todo list to track progress and ensure nothing is missed.

### Use Sub-Agents
When possible, use sub-agents (Task tool) to handle specialized work in parallel:
- Use the **Explore** agent to search and understand the codebase
- Use the **Bash** agent for git operations and command execution
- Use the **Plan** agent for designing implementation strategies
- Run independent tasks concurrently to maximize efficiency
