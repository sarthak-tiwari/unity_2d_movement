## intent:move_bot
- move <!-- no entity -->
- go <!-- no entity -->
- move [forward](direction:fwd)
- move [fwd](direction)
- go [forward](direction:fwd)
- go [fwd](direction)
- move [backward](direction:bck)
- move [back](direction:bck)
- move [bck](direction)
- go [backward](direction:bck)
- go [back](direction)
- go [bck](direction)
- move [left](direction:lft)
- move [lft](direction)
- go [left](direction:lft)
- go [lft](direction)
- move [right](direction:rght)
- move [rght](direction)
- go [right](direction:rght)
- go [rght](direction)

- move [2](magnitude) blocks
- go [31](magnitude) blocks
- move [forward](direction:fwd) [4](magnitude) steps
- move [56](magnitude) units [fwd](direction)
- go [forward](direction:fwd) [77](magnitude) step
- go  [8](magnitude) unit [fwd](direction)
- move [backward](direction:bck) [19](magnitude) block
- move [back](direction:bck) [19](magnitude) block
- move [79](magnitude) steps in [bck](direction)
- go [backward](direction:bck) [69](magnitude) steps
- go [back](direction:bck) [69](magnitude) steps
- go [bck](direction) [43](magnitude) units
- move [21](magnitude) steps to the [left](direction:lft)
- move [lft](direction) [93](magnitude) blocks
- go [87](magnitude) steps [left](direction:lft)
- go [lft](direction) [72](magnitude) units
- move [17](magnitude) blocks in [right](direction:rght)
- move [rght](direction) [3](magnitude) steps
- go [99](magnitude) blocks towards [right](direction:rght)
- go [rght](direction) [7](magnitude) units

## intent:see_bot
- look
- look [forward](direction:fwd)
- look [back](direction:bck)
- look [left](direction:lft)
- look [right](direction:rght)

## intent:turn_bot
- turn
- turn [forward](direction:fwd)
- turn [back](direction:bck)
- turn [left](direction:lft)
- turn [right](direction:rght)

## lookup:direction
- forward
- backward
- left
- right
- fwd
- rght
- lft
- bck

## synonym:fwd
- forward
- straight
- front

## synonym:bck
- back
- backward
- reverse
- behind