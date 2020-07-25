# Description

*Unity Animation And Physics Binding* - is attempt to make simple tool for blending animations with physic.

Unity - 2020.1



## How to use

* You should create two game objects: one for reference animation (for example using Animator Controller, or script), second for physic (for example with rigidbody)
* Add script `AnimationBlender` to any root bone you want to animate. `AnimationBlender` will apply animation from reference animation object to this object and all his children.
* Setup all parameters
  * `Reference` - object for taking animation
  * `Check Bones Names` - Should we fail if bones name are not equal
  * `Animation Factor` - How much animation updates should be applied
  * `Resolve Factor` - How much physic body should try to return to animation angles
  * `Ingore` - A list of Transforms to ignore (animation will not be applied)

<img src="Doc/Animation Blender.png" width=400>

## What sholud be enhanced

* To make character stay in the place you need to ignore one bone, for example, hip and make its rigidboby Kinematic. It will allow the animation
* Joints should be set up depends on your animation. Movement of your animations will be not so intensive if it is not allowed by joints
* Physic will affect the doll, even if you will set all parameters to the maximum. So you will never have animations that look 100% as original
* Algorithm depends on bones' game objects transforms order, names, and hierarchy. It will not work if it is not equals
