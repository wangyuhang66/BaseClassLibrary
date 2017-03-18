#ifndef __ACTION_H__
#define __ACTION_H__

namespace ons{
//consuming result
    public enum  class Action{
    //consume success, application could continue to consume next message
        CommitMessage =0,
    //consume fail, server will deliver this message later, application could continue to consume next message
        ReconsumeLater=1
    };
}
#endif